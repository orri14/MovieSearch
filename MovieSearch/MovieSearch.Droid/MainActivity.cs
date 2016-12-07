﻿using Android.App;
using Android.Runtime;
using Android.Widget;
using Android.OS;

namespace MovieSearch.Droid
{
    using Android.Support.Design.Widget;
    using Android.Support.V4.App;
    using Android.Support.V4.View;

    [Activity(Theme = "@style/MyTheme", Label = "MovieSearch.Droid", Icon = "@drawable/icon")]
    public class MainActivity : FragmentActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            this.SetContentView(Resource.Layout.Main);

            var fragments = new Fragment[]
                                {
                                    new TitleSearchFragment(),
                                    new OtherFragment()
                                };
            var titles = CharSequence.ArrayFromStringArray(new[]
                                                               {
                                                                   "Search",
                                                                   "Other"
                                                               });

            var viewPager = this.FindViewById<ViewPager>(Resource.Id.viewpager);
            viewPager.Adapter = new TabsFragmentPagerAdapter(SupportFragmentManager, fragments, titles);

            // Give the TabLayout the ViewPager
            var tabLayout = this.FindViewById<TabLayout>(Resource.Id.sliding_tabs);
            tabLayout.SetupWithViewPager(viewPager);

            var toolbar = this.FindViewById<Toolbar>(Resource.Id.toolbar);
            this.SetActionBar(toolbar);
            this.ActionBar.Title = "MovieSearch";
        }
    }
}