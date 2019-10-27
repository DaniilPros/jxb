using System;
using System.Collections.Generic;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using JXB.Model;

namespace JXB.Mobile.Android.Adapters
{
    class UsersAdapter : RecyclerView.Adapter
    {
        public List<UserVm> Users { get; set; }

        public override int ItemCount => Users != null ? Users.Count : 0;

        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            var viewHolder = holder as UserViewHolder;
            viewHolder.Name = Users[position].Name.Substring(0, 1);
        }

        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            return new UserViewHolder(
                LayoutInflater.From(parent.Context).Inflate(Resource.Layout.UserItem, parent, false));
        }
    }

    class UserViewHolder : RecyclerView.ViewHolder
    {
        public string Name
        {
            get => name.Text;
            set => name.Text = value;
        }

        TextView name;
        public UserViewHolder(View itemView) : base(itemView)
        {
            name = itemView.FindViewById<TextView>(Resource.Id.Name);
        }
    }
}