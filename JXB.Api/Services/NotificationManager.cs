using Microsoft.Azure.NotificationHubs;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using JXB.Api.Notification;

namespace JXB.Api.Services
{
    public class NotificationManager
    {
        NotificationHubClient Hub;
        NotificationHubConfig config;
        List<UserDevice> registeredDevices;

        object devicesLock = new object();

        public NotificationManager(IOptions<NotificationHubConfig> hubConfigOptions)
        {
            config = hubConfigOptions.Value;
            Hub = NotificationHubClient.CreateClientFromConnectionString(config.NotificationHub, config.NotificationHubName);
            registeredDevices = new List<UserDevice>();
        }

        public async Task Register(Registration registration)
        {
            if (registration.Handle == null)
            {
                throw new ArgumentNullException("Registration handle is null");
            }
            try
            {
                var registrations = await Hub.GetRegistrationsByChannelAsync(registration.Handle, 0);

                foreach (var regDescription in registrations)
                {
                    await Hub.DeleteRegistrationAsync(regDescription);
                }

                foreach (var key in registration.Keys)
                {
                    await Unregister(key);
                }
                await RemoveOld(registration.Handle);

                RegistrationDescription reg = await Hub.CreateFcmNativeRegistrationAsync(registration.Handle, registration.Keys);

                lock (devicesLock)
                {
                    Debug.WriteLine("registered " + registration.Keys);
                    registeredDevices.Add(new UserDevice
                    {
                        Registration = registration,
                        RegistrationID = reg.RegistrationId
                    });
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine("REGISTER " + e.Message);
                throw e;
            }
        }
        public async Task Unregister(string tag)
        {
            Debug.WriteLine("unregister " + tag);
            if (string.IsNullOrEmpty(tag))
            {
                return;
            }
            await DeleteRegistrationsOnHub(Hub, tag);
            lock (devicesLock)
            {
                if (registeredDevices.Any(item => item.Registration.Keys.Contains(tag)))
                {
                    registeredDevices.RemoveAll(item => item.Registration.Keys.Contains(tag));
                }
            }
        }

        async Task DeleteRegistrationsOnHub(NotificationHubClient hub, string tag)
        {
            try
            {
                var registrations = await hub.GetRegistrationsByTagAsync(tag, 0);
                foreach (RegistrationDescription registration in registrations)
                {
                    await hub.DeleteRegistrationAsync(registration);
                }
            }
            catch (Exception e)
            {
            }
        }

        async Task DeleteRegistrationsOnHubByID(NotificationHubClient hub, string id)
        {
            try
            {
                await hub.DeleteRegistrationAsync(id);
            }
            catch (Exception e)
            {
            }
        }

        async Task RemoveOld(string handle)
        {
            IEnumerable<UserDevice> devs;
            lock (devicesLock)
            {
                devs = (from device in registeredDevices where device.Registration.Handle == handle select device);
            }
            if (devs != null && devs.Count() > 0)
            {
                foreach (var dev in devs)
                {
                    await DeleteRegistrationsOnHubByID(Hub, dev.RegistrationID);
                }
                lock (devicesLock)
                {
                    registeredDevices.RemoveAll(item => item.Registration.Handle == handle);
                }
            }

        }

        public string ConstructMessage(string message)
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(new AndroidNotificationPayload()
            {
                notification = new AndroidNotificationPayload.Notification()
                {
                    title_loc_key = "",
                    body_loc_key = message
                },
                collapseKey = message,
                data = new AndroidNotificationPayload.Data()
                {
                    action = "updated",
                    data = message
                }
            });
        }
        
        public async Task SendMessage(UserDevice device, string message)
        {
            try
            {
                NotificationOutcome outcome = await Hub.SendFcmNativeNotificationAsync(message, device.Registration.Keys.FirstOrDefault());
            }
            catch (Exception e)
            {
            }
        }

        public List<UserDevice> GetDevicesForTags(List<string> tags)
        {
            List<UserDevice> devices = new List<UserDevice>();
            lock (devicesLock)
            {
                devices = (from device
                        in registeredDevices
                    where device.Registration.Keys.Intersect(tags).Count() > 0
                    select device).ToList();
            }
            return devices;
        }
    }
}