using System;
using System.Collections.Generic;
using Android.Support.Design.Button;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using JXB.Model;

namespace JXB.Mobile.Android.Adapters
{
    class QuestionsAdapter : RecyclerView.Adapter, IClickDelegate
    {
        enum ViewTypes { Question, Done}

        public List<QuestionVm> Questions { get; set; }
        public event Action<int, Answer> OnSelected;

        public override int ItemCount => Questions  == null ? 0 : Questions.Count + 1;

        public override int GetItemViewType(int position)
        {
            if (position < Questions.Count)
            {
                return (int)ViewTypes.Question;
            }
            else  return (int)ViewTypes.Done;
        }
        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            if (holder is QuestionsViewHolder viewHolder)
            {
                var item = Questions[position];
                //viewHolder.Title = item.;
                viewHolder.Left = item.Option1;
                viewHolder.Right = item.Option2;
                viewHolder.Id = position;
                viewHolder.ClickDelegate = this;
            }
        }

        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            if (viewType == (int)ViewTypes.Question)
            {
                return new QuestionsViewHolder(
                    LayoutInflater.From(parent.Context).Inflate(Resource.Layout.QuestionItem, parent, false));
            }
            return new DoneViewHolder(
                    LayoutInflater.From(parent.Context).Inflate(Resource.Layout.DoneItem, parent, false));
        }

        void IClickDelegate.OnSelected(int id, Answer answer)
        {
            OnSelected?.Invoke(id, answer);
        }
    }

    class QuestionsViewHolder : RecyclerView.ViewHolder
    {
        public string Title
        {
            get => title.Text;
            set => title.Text = value;
        }
        public string Left
        {
            get => left.Text;
            set => left.Text = value;
        }
        public string Right
        {
            get => right.Text;
            set => right.Text = value;
        }

        public int Id { get; set; }

        public IClickDelegate ClickDelegate { get; set; }

        TextView title;
        MaterialButton left;
        MaterialButton right;
        public QuestionsViewHolder(View itemView) : base(itemView)
        {
            title = itemView.FindViewById<TextView>(Resource.Id.Title);
            title.Visibility = ViewStates.Gone;
            left = itemView.FindViewById<MaterialButton>(Resource.Id.Left);
            right = itemView.FindViewById<MaterialButton>(Resource.Id.Right);
            left.Click += Left_Click;
            right.Click += Right_Click;
        }

        private void Right_Click(object sender, EventArgs e)
        {
            ClickDelegate.OnSelected(Id, Answer.Option2);
        }

        private void Left_Click(object sender, EventArgs e)
        {
            ClickDelegate.OnSelected(Id, Answer.Option1);
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            right.Click -= Right_Click;
            left.Click -= Left_Click;
        }
    }

    public class DoneViewHolder : RecyclerView.ViewHolder
    {
        public DoneViewHolder(View itemView) : base(itemView)
        {
        }
    }

    public interface IClickDelegate
    {
        void OnSelected(int id, Answer answer);
    }
}