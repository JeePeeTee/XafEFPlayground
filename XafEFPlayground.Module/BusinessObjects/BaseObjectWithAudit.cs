#region MIT License

// ==========================================================
// 
// XafEFPlayground project - Copyright (c) 2022 JeePeeTee
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

using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Filtering;
using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl.EF;
using XafEFPlayground.Module.Auditing;
using XafEFPlayground.Module.Entities;

#endregion

namespace XafEFPlayground.Module.BusinessObjects;

[Serializable]
public abstract class BaseObjectWithAudit : BaseObject, IHaveConcurrencyStamp, IAuditedObject {
    [VisibleInListView(false)]
    [VisibleInDetailView(false)]
    [VisibleInLookupListView(false)]
    // BUG Not Support with EF!?
    [NonCloneable]
    [SearchMemberOptions(SearchMemberMode.Exclude)]
    [ModelDefault("DisplayFormat", "{0:G}")]
    [ModelDefault("AllowEdit", "False")]
    public virtual DateTime? Created { get; set; }

    [VisibleInListView(false)]
    [VisibleInDetailView(false)]
    [VisibleInLookupListView(false)]
    [NonCloneable]
    [SearchMemberOptions(SearchMemberMode.Exclude)]
    [ModelDefault("AllowEdit", "False")]
    public virtual string CreatedBy { get; set; }

    [VisibleInListView(false)]
    [VisibleInDetailView(false)]
    [VisibleInLookupListView(false)]
    [NonCloneable]
    [SearchMemberOptions(SearchMemberMode.Exclude)]
    [ModelDefault("DisplayFormat", "{0:G}")]
    [ModelDefault("AllowEdit", "False")]
    public virtual DateTime? LastModified { get; set; }

    [VisibleInListView(false)]
    [VisibleInDetailView(false)]
    [VisibleInLookupListView(false)]
    [NonCloneable]
    [SearchMemberOptions(SearchMemberMode.Exclude)]
    [ModelDefault("AllowEdit", "False")]
    public virtual string LastModifiedBy { get; set; }

    [VisibleInListView(false)]
    [VisibleInDetailView(false)]
    [VisibleInLookupListView(false)]
    [NonCloneable]
    [SearchMemberOptions(SearchMemberMode.Exclude)]
    [ModelDefault("DisplayFormat", "{0:G}")]
    [ModelDefault("AllowEdit", "False")]
    public virtual DateTime? Deleted { get; set; }

    [VisibleInListView(false)]
    [VisibleInDetailView(false)]
    [VisibleInLookupListView(false)]
    [NonCloneable]
    [SearchMemberOptions(SearchMemberMode.Exclude)]
    [ModelDefault("AllowEdit", "False")]
    public virtual string DeletedBy { get; set; }

    [VisibleInListView(false)]
    [VisibleInDetailView(false)]
    [VisibleInLookupListView(false)]
    [NonCloneable]
    public virtual bool IsDeleted { get; set; }

    [DisableAuditing]
    [VisibleInListView(false)]
    [VisibleInDetailView(false)]
    [VisibleInLookupListView(false)]
    [NonCloneable]
    public virtual byte[] Version { get; set; }

    private static string GetCurrentUserName() => SecuritySystem.Instance?.UserName;

    /* Behaviour moved to DBContext 12-7-2023
    public override void OnSaving() {
        base.OnSaving();

        if (ObjectSpace.IsNewObject(this)) {
            Created = DateTime.Now;
            CreatedBy = GetCurrentUserName() ?? "(Automated)";
        }
        // OnDeleting does some extra tricks via Global OnSaving with SoftDelete
        else if (ObjectSpace.IsObjectToDelete(this)) {
            Deleted = DateTime.Now;
            DeletedBy = GetCurrentUserName() ?? "(Automated)";
        }
        else {
            LastModified = DateTime.Now;
            LastModifiedBy = GetCurrentUserName() ?? "(Automated)";
        }
    }
    */

    public override void OnLoaded() {
        base.OnLoaded();
    }
}