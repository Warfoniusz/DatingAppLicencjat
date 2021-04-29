using Android.Graphics;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using DatingAppLicencjat.Models;
using Refractored.Controls;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;

namespace DatingAppLicencjat.Adapters
{
    class InterestedUsersAdapter : RecyclerView.Adapter
    {
        public event EventHandler<InterestedUsersAdapterClickEventArgs> acceptClick;
        public event EventHandler<InterestedUsersAdapterClickEventArgs> denyClick;
        public event EventHandler<InterestedUsersAdapterClickEventArgs> ItemClick;
        public event EventHandler<InterestedUsersAdapterClickEventArgs> ItemLongClick;
        List<userProfileModel> userList;

        public InterestedUsersAdapter(List<userProfileModel> data)
        {
            userList = data;
        }

        // Create new views (invoked by the layout manager)
        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {

            //Setup your layout here
            View itemView = null;


            itemView = LayoutInflater.From(parent.Context).Inflate(Resource.Layout.user_interested_selection_template, parent, false);

            var vh = new InterestedUsersAdapterViewHolder(itemView, OnClick, OnLongClick, onAcceptClick, onDenyClick);
            return vh;
        }

        // Replace the contents of a view (invoked by the layout manager)
        public override void OnBindViewHolder(RecyclerView.ViewHolder viewHolder, int position)
        {
            var user = userList[position];

            // Replace the contents of the view with that element
            var holder = viewHolder as InterestedUsersAdapterViewHolder;
            holder.username_in_interested_choice.Text = user.username;
            Byte[] bitmapData = Convert.FromBase64String(user.profile_photo_base64);
            System.IO.MemoryStream ms = new System.IO.MemoryStream(bitmapData);
            Android.Graphics.Bitmap bitImage = Android.Graphics.BitmapFactory.DecodeStream(ms);
            holder.user_photo_in_interested_choice.SetImageBitmap(bitImage);

            
            //holder.TextView.Text = items[position];
        }

        public override int ItemCount => userList.Count;

        void OnClick(InterestedUsersAdapterClickEventArgs args) => ItemClick?.Invoke(this, args);
        void OnLongClick(InterestedUsersAdapterClickEventArgs args) => ItemLongClick?.Invoke(this, args);
        void onAcceptClick(InterestedUsersAdapterClickEventArgs args) => acceptClick?.Invoke(this, args);
        void onDenyClick(InterestedUsersAdapterClickEventArgs args) => denyClick?.Invoke(this, args);


    }

    public class InterestedUsersAdapterViewHolder : RecyclerView.ViewHolder
    {
        public TextView username_in_interested_choice;
        public CircleImageView user_photo_in_interested_choice;
        public ImageView accept_user_in_interested_choice;
        public ImageView deny_user_in_interested_choice;


        public InterestedUsersAdapterViewHolder(View itemView, Action<InterestedUsersAdapterClickEventArgs> clickListener,
                            Action<InterestedUsersAdapterClickEventArgs> longClickListener, Action<InterestedUsersAdapterClickEventArgs> acceptClickListener,
                                Action<InterestedUsersAdapterClickEventArgs> denyClickListener) : base(itemView)
        {
            username_in_interested_choice = (TextView)itemView.FindViewById(Resource.Id.username_text_in_interested_choice);
            user_photo_in_interested_choice = (CircleImageView)itemView.FindViewById(Resource.Id.user_photo_in_interested_choice);
            accept_user_in_interested_choice = (ImageView)itemView.FindViewById(Resource.Id.accept_in_interested_choice);
            deny_user_in_interested_choice = (ImageView)itemView.FindViewById(Resource.Id.deny_in_interested_choice);
            itemView.Click += (sender, e) => clickListener(new InterestedUsersAdapterClickEventArgs { View = itemView, Position = AdapterPosition });
            itemView.LongClick += (sender, e) => longClickListener(new InterestedUsersAdapterClickEventArgs { View = itemView, Position = AdapterPosition });
            accept_user_in_interested_choice.Click += (sender, e) => acceptClickListener(new InterestedUsersAdapterClickEventArgs { View = itemView, Position = AdapterPosition });
            deny_user_in_interested_choice.Click += (sender, e) => denyClickListener(new InterestedUsersAdapterClickEventArgs { View = itemView, Position = AdapterPosition });

        }
    }

    public class InterestedUsersAdapterClickEventArgs : EventArgs
    {
        public View View { get; set; }
        public int Position { get; set; }
    }
}