﻿

#pragma checksum "C:\GitHub\Library Project\LibraryProject\LibraryProject\LibraryProject.Windows\CustomerMainPage.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "95F2F5BCD4F2EE6D6B55815519095709"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace LibraryProject
{
    partial class CustomerMainPage : global::Windows.UI.Xaml.Controls.Page, global::Windows.UI.Xaml.Markup.IComponentConnector
    {
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 4.0.0.0")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
 
        public void Connect(int connectionId, object target)
        {
            switch(connectionId)
            {
            case 1:
                #line 17 "..\..\CustomerMainPage.xaml"
                ((global::Windows.UI.Xaml.Controls.Primitives.ButtonBase)(target)).Click += this.logOutBtn_Click;
                 #line default
                 #line hidden
                break;
            case 2:
                #line 35 "..\..\CustomerMainPage.xaml"
                ((global::Windows.UI.Xaml.Controls.SearchBox)(target)).QuerySubmitted += this.searchItemBox_QuerySubmitted;
                 #line default
                 #line hidden
                break;
            case 3:
                #line 36 "..\..\CustomerMainPage.xaml"
                ((global::Windows.UI.Xaml.Controls.Primitives.ButtonBase)(target)).Click += this.rentBookBtn_Click;
                 #line default
                 #line hidden
                break;
            case 4:
                #line 37 "..\..\CustomerMainPage.xaml"
                ((global::Windows.UI.Xaml.Controls.Primitives.ButtonBase)(target)).Click += this.returnBookBtn_Click;
                 #line default
                 #line hidden
                break;
            case 5:
                #line 38 "..\..\CustomerMainPage.xaml"
                ((global::Windows.UI.Xaml.Controls.Primitives.ButtonBase)(target)).Click += this.showRentedBtn_Click;
                 #line default
                 #line hidden
                break;
            }
            this._contentLoaded = true;
        }
    }
}


