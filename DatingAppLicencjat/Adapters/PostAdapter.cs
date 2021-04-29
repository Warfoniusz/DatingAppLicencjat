using Android.Graphics;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using DatingAppLicencjat.Models;
using System;
using System.Collections.Generic;

namespace DatingAppLicencjat.Adapters
{
    class PostAdapter : RecyclerView.Adapter
    {
        public event EventHandler<Adapter1ClickEventArgs> ItemClick;
        public event EventHandler<Adapter1ClickEventArgs> ItemLongClick;
        List<postModel> items;

        public PostAdapter(List<postModel> data)
        {
            items = data;
        }

        // Create new views (invoked by the layout manager)
        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            //Setup your layout here
            View itemView = null;
            itemView = LayoutInflater.From(parent.Context).Inflate(Resource.Layout.userPost, parent, false);
            var vh = new Adapter1ViewHolder(itemView, OnClick, OnLongClick);
            return vh;
        }

        // Replace the contents of a view (invoked by the layout manager)
        public override void OnBindViewHolder(RecyclerView.ViewHolder viewHolder, int position)
        {
            var item = items[position];

            // Replace the contents of the view with that element
            var holder = viewHolder as Adapter1ViewHolder;
            //holder.TextView.Text = items[position];
            holder.usernameTextView.Text = item.username;
            holder.postBodyTextView.Text = item.title;
            holder.cityTextView.Text = item.city;
            if (item.acceptanceStatus == "waiting")
            {
                holder.followedPost.SetBackgroundColor(Color.ParseColor("#ebde34"));
            }
            else if (item.acceptanceStatus == "denied")
            {
                holder.followedPost.SetBackgroundColor(Color.ParseColor("#fc0330"));
            }
            else if (item.acceptanceStatus == "accepted")
            {
                holder.followedPost.SetBackgroundColor(Color.ParseColor("#03fc5a"));
            }
        }

        public override int ItemCount => items.Count;

        void OnClick(Adapter1ClickEventArgs args) => ItemClick?.Invoke(this, args);
        void OnLongClick(Adapter1ClickEventArgs args) => ItemLongClick?.Invoke(this, args);
    }

    public class Adapter1ViewHolder : RecyclerView.ViewHolder
    {
        public TextView usernameTextView { get; set; }
        public TextView postBodyTextView { get; set; }
        public TextView cityTextView { get; set; }
        public ImageView postImageView { get; set; }
        public CardView followedPost { get; set; }

        public Adapter1ViewHolder(View itemView, Action<Adapter1ClickEventArgs> clickListener,
            Action<Adapter1ClickEventArgs> longClickListener) : base(itemView)
        {
            followedPost = itemView.FindViewById<CardView>(Resource.Id.postInViewed);
            usernameTextView = itemView.FindViewById<TextView>(Resource.Id.userPostNickname);
            postBodyTextView = itemView.FindViewById<TextView>(Resource.Id.userPostDescription);
            cityTextView = itemView.FindViewById<TextView>(Resource.Id.userPostCity);
            postImageView = itemView.FindViewById<ImageView>(Resource.Id.postPhoto);
            itemView.Click += (sender, e) => clickListener(new Adapter1ClickEventArgs
                {View = itemView, Position = AdapterPosition});
            itemView.LongClick += (sender, e) => longClickListener(new Adapter1ClickEventArgs
                {View = itemView, Position = AdapterPosition});
        }
    }

    public class Adapter1ClickEventArgs : EventArgs
    {
        public View View { get; set; }
        public int Position { get; set; }
    }
}