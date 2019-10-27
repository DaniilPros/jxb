using System;
using System.Collections.Generic;
using System.Linq;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using JXB.Model;

namespace JXB.Mobile.Android.Adapters
{
    class ActivityAdapter : RecyclerView.Adapter
    {
        enum ViewTypes { Title, Users, Responsibilities }

        public ActivityVm Activity { get; set; }

        public override int ItemCount
        {
            get
            {
                if (Activity != null)
                {
                    if (DateTimeOffset.UtcNow < Activity.Time + TimeSpan.FromMinutes(2))
                    {
                        return 3;
                    }
                    else
                    {
                        return 1;
                    }
                }
                return 0;
            }
        }

        public override int GetItemViewType(int position)
        {
            return position;
        }

        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            switch (GetItemViewType(position))
            {
                case (int)ViewTypes.Title:
                    {
                        var viewHolder = holder as TitleViewHolder;
                        viewHolder.Title = Activity.Name;
                        viewHolder.Time = Activity.Time.ToString();
                        break;
                    }
                case (int)ViewTypes.Users:
                    {
                        var viewHolder = holder as UsersViewHolder;
                        viewHolder.Users = Activity.Users?.ToList();
                        break;
                    }
                case (int)ViewTypes.Responsibilities:
                    {
                        var viewHolder = holder as ResponsibilitiesViewHolder;
                        if (Activity.Responsibilities != null && Activity.Responsibilities.Any())
                        {
                            var resp = new List<Responsibility>();
                            Activity.Responsibilities.ToList().ForEach((item) => resp.Add(new Responsibility
                            {
                                Name = item
                            }));
                            viewHolder.Responsibilities = resp;
                        }
                        break;
                    }
            }
        }

        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            switch (viewType)
            {
                case (int)ViewTypes.Title:
                    return new TitleViewHolder(
                        LayoutInflater.From(parent.Context).Inflate(Resource.Layout.TitleItem, parent, false));
                case (int)ViewTypes.Users:
                    return new UsersViewHolder(
                        LayoutInflater.From(parent.Context).Inflate(Resource.Layout.UsersItem, parent, false));
                case (int)ViewTypes.Responsibilities:
                    return new ResponsibilitiesViewHolder(
                        LayoutInflater.From(parent.Context).Inflate(Resource.Layout.ResponsibilitiesItem, parent, false));
            }
            throw new InvalidOperationException($"This ViewType is not supported {viewType}");
        }

        class TitleViewHolder : RecyclerView.ViewHolder
        {
            public string Title
            {
                get => title.Text;
                set => title.Text = value;
            }

            public string Time
            {
                get => time.Text;
                set => time.Text = value;
            }

            TextView title;
            TextView time;
            public TitleViewHolder(View itemView) : base(itemView)
            {
                title = itemView.FindViewById<TextView>(Resource.Id.Title);
                time = itemView.FindViewById<TextView>(Resource.Id.Time);
            }
        }

        class UsersViewHolder : RecyclerView.ViewHolder
        {
            private UsersAdapter adapter;

            public List<UserVm> Users
            {
                get => adapter.Users;
                set
                {
                    adapter.Users = value;
                    adapter.NotifyDataSetChanged();
                }
            }
            public UsersViewHolder(View itemView) : base(itemView)
            {
                var list = itemView.FindViewById<RecyclerView>(Resource.Id.Users);
                var lm = new GridLayoutManager(itemView.Context, 4);
                list.SetLayoutManager(lm);

                adapter = new UsersAdapter();
                list.SetAdapter(adapter);
            }
        }

        class ResponsibilitiesViewHolder : RecyclerView.ViewHolder
        {
            public List<Responsibility> Responsibilities
            {
                get => adapter.Responsibilities;
                set
                {
                    adapter.Responsibilities = value;
                    adapter.NotifyDataSetChanged();
                }
            }
            ResponsibilitiesAdapter adapter;
            public ResponsibilitiesViewHolder(View itemView) : base(itemView)
            {
                var list = itemView.FindViewById<RecyclerView>(Resource.Id.Responsibilities);
                var lm = new LinearLayoutManager(itemView.Context, LinearLayoutManager.Vertical, false);
                list.SetLayoutManager(lm);

                adapter = new ResponsibilitiesAdapter();
                list.SetAdapter(adapter);
            }
        }
    }

    public class Responsibility
    {
        public string Name { get; set; }
        public UserVm User { get; set; }
    }
}