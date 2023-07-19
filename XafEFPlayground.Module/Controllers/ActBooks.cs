#region MIT License

// ==========================================================
// 
// XafEFPlayground project - Copyright (c) 2023 XAFers Arizona User Group
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in all
// copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
// SOFTWARE.
// 
// ===========================================================

#endregion

#region usings

using System.ComponentModel;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using DevExpress.ExpressApp.EFCore;
using DevExpress.Persistent.Base;
using Microsoft.EntityFrameworkCore;
using XafEFPlayground.Module.BusinessObjects;

#endregion

namespace XafEFPlayground.Module.Controllers;

public class ActBooks : ViewController {
    private readonly IContainer components;

    public ActBooks() {
        this.components = new Container();
        InitializeCreateData();
    }


    protected override void OnActivated() {
        base.OnActivated();
        // Perform various tasks depending on the target View.
    }

    protected override void OnViewControlsCreated() {
        base.OnViewControlsCreated();
        // Access and customize the target View control.
    }

    protected override void OnDeactivated() {
        // Unsubscribe from previously subscribed events and release other references and resources.
        base.OnDeactivated();
    }

    protected override void Dispose(bool disposing) {
        if (disposing) {
            foreach (var action in this.Actions) {
                action.Dispose();
            }

            components?.Dispose();
        }

        base.Dispose(disposing);
    }

    // New actions here... check templates actsmpl, act...

    #region Simple action: CreateData

    private SimpleAction _createData;

    private void InitializeCreateData() {
        _createData = new SimpleAction(this, nameof(_createData), PredefinedCategory.View) {
            Caption = "Create Data",
            ConfirmationMessage = null,
            ImageName = "ModelEditor_Application",
            SelectionDependencyType = SelectionDependencyType.Independent,
            ToolTip = null,
            TargetObjectType = typeof(Book),
            TargetViewType = ViewType.Any,
            TargetViewNesting = Nesting.Any,
            TargetObjectsCriteriaMode = TargetObjectsCriteriaMode.TrueAtLeastForOne,
        };

        _createData.Execute += CreateDataExecute;

        this.Actions.Add(_createData);

        void CreateDataExecute(object sender, SimpleActionExecuteEventArgs e) {
            using var ios = Application.CreateObjectSpace(typeof(Book));

            for (var i = 1; i <= 5000; i++) {
                var record = ios.CreateObject<Book>();
                record.BookTitle = $"Book {i}";
                record.BasePrice = 9.95m;
                record.PublishDate = DateTime.Today;
            }

            ios.CommitChanges();

            View.Refresh(true);
        }
    }

    #endregion Simple action: CreateData
}