﻿#pragma checksum "..\..\..\Pages\Contacts.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "BD258B55D830DB50CACE3BDA2509EEAE57DD58D2"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using Eskuvo_tervezo.Pages;
using Eskuvo_tervezo.Properties;
using MaterialDesignThemes.Wpf;
using MaterialDesignThemes.Wpf.Converters;
using MaterialDesignThemes.Wpf.Transitions;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms.Integration;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Media.TextFormatting;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Shell;


namespace Eskuvo_tervezo.Pages {
    
    
    /// <summary>
    /// Contacts
    /// </summary>
    public partial class Contacts : System.Windows.Controls.Page, System.Windows.Markup.IComponentConnector {
        
        
        #line 43 "..\..\..\Pages\Contacts.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label LB_Contact;
        
        #line default
        #line hidden
        
        
        #line 46 "..\..\..\Pages\Contacts.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label LB_Name;
        
        #line default
        #line hidden
        
        
        #line 50 "..\..\..\Pages\Contacts.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox TB_Name;
        
        #line default
        #line hidden
        
        
        #line 54 "..\..\..\Pages\Contacts.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label LB_Phone;
        
        #line default
        #line hidden
        
        
        #line 58 "..\..\..\Pages\Contacts.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox TB_Phone;
        
        #line default
        #line hidden
        
        
        #line 62 "..\..\..\Pages\Contacts.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label LB_Email;
        
        #line default
        #line hidden
        
        
        #line 66 "..\..\..\Pages\Contacts.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox TB_Email;
        
        #line default
        #line hidden
        
        
        #line 71 "..\..\..\Pages\Contacts.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button BT_Save;
        
        #line default
        #line hidden
        
        
        #line 76 "..\..\..\Pages\Contacts.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.StackPanel ContactItems;
        
        #line default
        #line hidden
        
        
        #line 81 "..\..\..\Pages\Contacts.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button BT_ExportToExcel;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/Eskuvo_tervezo;component/pages/contacts.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\Pages\Contacts.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            
            #line 13 "..\..\..\Pages\Contacts.xaml"
            ((Eskuvo_tervezo.Pages.Contacts)(target)).Loaded += new System.Windows.RoutedEventHandler(this.Page_Loaded);
            
            #line default
            #line hidden
            return;
            case 2:
            this.LB_Contact = ((System.Windows.Controls.Label)(target));
            return;
            case 3:
            this.LB_Name = ((System.Windows.Controls.Label)(target));
            return;
            case 4:
            this.TB_Name = ((System.Windows.Controls.TextBox)(target));
            
            #line 51 "..\..\..\Pages\Contacts.xaml"
            this.TB_Name.PreviewKeyDown += new System.Windows.Input.KeyEventHandler(this.TB_Name_PreviewKeyDown);
            
            #line default
            #line hidden
            return;
            case 5:
            this.LB_Phone = ((System.Windows.Controls.Label)(target));
            return;
            case 6:
            this.TB_Phone = ((System.Windows.Controls.TextBox)(target));
            
            #line 59 "..\..\..\Pages\Contacts.xaml"
            this.TB_Phone.PreviewKeyDown += new System.Windows.Input.KeyEventHandler(this.TB_Phone_PreviewKeyDown);
            
            #line default
            #line hidden
            return;
            case 7:
            this.LB_Email = ((System.Windows.Controls.Label)(target));
            return;
            case 8:
            this.TB_Email = ((System.Windows.Controls.TextBox)(target));
            
            #line 67 "..\..\..\Pages\Contacts.xaml"
            this.TB_Email.PreviewKeyDown += new System.Windows.Input.KeyEventHandler(this.TB_Email_PreviewKeyDown);
            
            #line default
            #line hidden
            return;
            case 9:
            this.BT_Save = ((System.Windows.Controls.Button)(target));
            
            #line 74 "..\..\..\Pages\Contacts.xaml"
            this.BT_Save.Click += new System.Windows.RoutedEventHandler(this.BT_Save_Click);
            
            #line default
            #line hidden
            return;
            case 10:
            this.ContactItems = ((System.Windows.Controls.StackPanel)(target));
            return;
            case 11:
            this.BT_ExportToExcel = ((System.Windows.Controls.Button)(target));
            
            #line 84 "..\..\..\Pages\Contacts.xaml"
            this.BT_ExportToExcel.Click += new System.Windows.RoutedEventHandler(this.BT_ExportToExcel_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

