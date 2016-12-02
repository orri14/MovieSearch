﻿using System;
using CoreGraphics;
using UIKit;

//WHAT THE WHAT!!

namespace MovieSearch.iOS.Controllers
{
    public class Spinner
    {
        public UIActivityIndicatorView activitySpinner;

        public Spinner(CGRect frame)
        {
            var centerX = frame.Width / 2;
            var centerY = frame.Height / 2;

            activitySpinner = new UIActivityIndicatorView(UIActivityIndicatorViewStyle.WhiteLarge);
            activitySpinner.Frame = new CGRect(
                centerX - activitySpinner.Frame.Width / 2,
                centerY - activitySpinner.Frame.Height - 20,
                activitySpinner.Frame.Width,
                activitySpinner.Frame.Height);
        }

    }
}