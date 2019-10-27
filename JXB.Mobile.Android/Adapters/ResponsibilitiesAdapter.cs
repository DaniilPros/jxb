using System;
using System.Collections.Generic;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;

namespace JXB.Mobile.Android.Adapters
{
    class ResponsibilitiesAdapter : RecyclerView.Adapter
    {
        public List<Responsibility> Responsibilities { get; set; }

        public override int ItemCount => Responsibilities != null ? Responsibilities.Count : 0;

        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            var viewHolder = holder as ResponsibilityViewHolder;
            viewHolder.Name = Responsibilities[position].Name;
        }

        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            return new ResponsibilityViewHolder(
                LayoutInflater.From(parent.Context).Inflate(Resource.Layout.ResponsibilityItem, parent, false));
        }
    }

    class ResponsibilityViewHolder : RecyclerView.ViewHolder
    {
        public string Name
        {
            get => name.Text;
            set => name.Text = value;
        }

        TextView name;
        public ResponsibilityViewHolder(View itemView) : base(itemView)
        {
            name = itemView.FindViewById<TextView>(Resource.Id.Name);
        }
    }
}