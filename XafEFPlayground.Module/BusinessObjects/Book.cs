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
using DevExpress.Persistent.Base;
using Microsoft.EntityFrameworkCore.Diagnostics;

#endregion

namespace XafEFPlayground.Module.BusinessObjects;

[DefaultClassOptions]
[ImageName("BO_Contact")]
[DefaultProperty(nameof(BookTitle))]
[CreatableItem(false)]
public class Book : BaseObjectWithAudit {
    public virtual string BookTitle { get; set; }
    public virtual DateTime? PublishDate { get; set; }
    public virtual decimal BasePrice { get; set; }
}

public class BookSavingChangesInterceptor : SaveChangesInterceptor {
    // Called at the start of DbContext.SaveChanges.
    public override InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result) {
        if (eventData?.Context is null ||
            !eventData.Context.ChangeTracker.Entries<Book>().Any())
            return base.SavingChanges(eventData, result);

        var oldBookTitle = eventData.Context.ChangeTracker.Entries<Book>().FirstOrDefault()?.Property(p => p.BookTitle).OriginalValue;
        var newBookTitle = eventData.Context.ChangeTracker.Entries<Book>().FirstOrDefault()?.Property(p => p.BookTitle).CurrentValue;

        return base.SavingChanges(eventData, result);
    }
}