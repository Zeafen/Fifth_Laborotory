﻿#pragma checksum "..\..\..\..\..\Pages\StoragePages\Collections\CollectionsPage.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "8819EED5A545FE19CEACF4C173D3B2922519EFB61526A710044DFB43C72CDCB6"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using Final_Practice.Pages.StoragePages.Collections;
using Final_Practice.Rules;
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


namespace Final_Practice.Pages.StoragePages.Collections {
    
    
    /// <summary>
    /// CollectionsPage
    /// </summary>
    public partial class CollectionsPage : System.Windows.Controls.Page, System.Windows.Markup.IComponentConnector {
        
        
        #line 50 "..\..\..\..\..\Pages\StoragePages\Collections\CollectionsPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGrid CollectionsDGr;
        
        #line default
        #line hidden
        
        
        #line 77 "..\..\..\..\..\Pages\StoragePages\Collections\CollectionsPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Grid InfoField;
        
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
            System.Uri resourceLocater = new System.Uri("/Final_Practice;component/pages/storagepages/collections/collectionspage.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\..\Pages\StoragePages\Collections\CollectionsPage.xaml"
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
            
            #line 33 "..\..\..\..\..\Pages\StoragePages\Collections\CollectionsPage.xaml"
            ((System.Windows.Controls.MenuItem)(target)).Click += new System.Windows.RoutedEventHandler(this.OnClearInfo_Click);
            
            #line default
            #line hidden
            return;
            case 2:
            
            #line 43 "..\..\..\..\..\Pages\StoragePages\Collections\CollectionsPage.xaml"
            ((System.Windows.Controls.MenuItem)(target)).Click += new System.Windows.RoutedEventHandler(this.OnSaveData_Click);
            
            #line default
            #line hidden
            return;
            case 3:
            this.CollectionsDGr = ((System.Windows.Controls.DataGrid)(target));
            return;
            case 4:
            this.InfoField = ((System.Windows.Controls.Grid)(target));
            return;
            case 5:
            
            #line 157 "..\..\..\..\..\Pages\StoragePages\Collections\CollectionsPage.xaml"
            ((System.Windows.Controls.Button)(target)).MouseRightButtonDown += new System.Windows.Input.MouseButtonEventHandler(this.Button_MouseRightButtonDown);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

