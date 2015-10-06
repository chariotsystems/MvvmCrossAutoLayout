# MvvmCrossAutoLayout

https://github.com/chariotsystems/MvvmCrossAutoLayout

Wouldn’t it be nice if you could auto layout an iOS app just like this?

            var Details = ProfileBorder.AddContainer ("Details", UIColor.White);
            var Photo = ProfileBorder.AddContainer ("Photo", UIColor.White);
            ProfileBorder.AddConstraint ("V:|-[Details]-|");
            ProfileBorder.AddConstraint ("V:|-[Photo]-(>=8)-|");
            ProfileBorder.AddConstraint ("H:|-[Details]-(>=8)-[Photo]-|");  

No clutter - just the views and the constraints

Well now you can:
https://github.com/chariotsystems/MvvmCrossAutoLayout

With a Wiki here:
https://github.com/chariotsystems/MvvmCrossAutoLayout/wiki


