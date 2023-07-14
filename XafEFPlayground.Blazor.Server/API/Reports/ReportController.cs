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

#nullable enable

#region usings

using DevExpress.ExpressApp.ReportsV2;
using DevExpress.Persistent.BaseImpl.EF;
using DevExpress.Xpo;
using DevExpress.Xpo.DB;
using DevExpress.XtraPrinting;
using DevExpress.XtraReports.UI;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

#endregion

namespace XafEFPlayground.Blazor.Server.API.Reports;

[Authorize]
[Route("api/[controller]")]
// This is a WebApi Reports controller sample.
public class ReportController : ControllerBase {
    private readonly IReportExportService service;

    public ReportController(IReportExportService reportExportService) {
        service = reportExportService;
    }

    private void ApplyParametersFromQuery(XtraReport report) {
        foreach (var parameter in report.Parameters) {
            var queryParam = Request.Query[parameter.Description];
            if (queryParam.Count > 0) {
                parameter.Value = queryParam.First();
            }
        }
    }

    private SortProperty[]? LoadSortPropertiesFromQuery() {
        if (Request.Query.Keys.Contains("sortProperty")) {
            var queryParam = Request.Query["sortProperty"];
            SortProperty[] result = new SortProperty[queryParam.Count];
            for (var i = 0; i < queryParam.Count; i++) {
                string[] paramData = queryParam[i].Split(",");
                result[i] = new SortProperty(paramData[0], (SortingDirection)Enum.Parse(typeof(SortingDirection), paramData[1]));
            }

            return result;
        }

        return null;
    }

    private async Task<object> GetReportContentAsync(XtraReport report, ExportTarget fileType) {
        var ms = await service.ExportReportAsync(report, fileType);
        HttpContext.Response.RegisterForDispose(ms);
        return File(ms, service.GetContentType(fileType), $"{report.DisplayName}.{service.GetFileExtension(fileType)}");
    }

    [HttpGet("DownloadByKey({key})")]
    [SwaggerOperation("Gets the contents of a report specified by its key in the specified file format filtered based on the specified condition.",
        "For more information, refer to the following article: <a href='https://docs.devexpress.com/eXpressAppFramework/404176/backend-web-api-service/obtain-a-report-from-a-web-api-controller-endpoint'>Obtain a Report from a Web API Controller Endpoint</a>.")]
    public async Task<object> DownloadByKey(
        [SwaggerParameter("A primary key value that uniquely identifies a report. <br/>Example: '83978d7f-82b7-4380-979a-08db4587a66b'")]
        string key,
        [FromQuery] [SwaggerParameter("The file type in which to download the report.")]
        ExportTarget fileType = ExportTarget.Pdf,
        [FromQuery] [SwaggerParameter("A condition used to filter the report's data. <br/>Example: \"[FirstName] = 'Aaron'\"")]
        string? criteria = null) {
        using var report = service.LoadReport<ReportDataV2>(key);
        ApplyParametersFromQuery(report);
        SortProperty[]? sortProperties = LoadSortPropertiesFromQuery();
        service.SetupReport(report, criteria, sortProperties);
        return await GetReportContentAsync(report, fileType);
    }

    [HttpGet("DownloadByName({displayName})")]
    [SwaggerOperation("Gets the contents of a report specified by its display name in the specified file format filtered based on the specified condition.",
        "For more information, refer to the following article: <a href='https://docs.devexpress.com/eXpressAppFramework/404176/backend-web-api-service/obtain-a-report-from-a-web-api-controller-endpoint'>Obtain a Report from a Web API Controller Endpoint</a>.")]
    public async Task<object> DownloadByName(
        [SwaggerParameter("The display name of a report to download. <br/>Example: 'Employee List Report'")]
        string displayName,
        [FromQuery] [SwaggerParameter("The file type in which to download the report.")]
        ExportTarget fileType = ExportTarget.Pdf,
        [FromQuery] [SwaggerParameter("A condition used to filter the report's data. <br/>Example: \"[FirstName] = 'Aaron'\"")]
        string? criteria = null) {
        if (!string.IsNullOrEmpty(displayName)) {
            using var report = service.LoadReport<ReportDataV2>(data => data.DisplayName == displayName);
            ApplyParametersFromQuery(report);
            SortProperty[]? sortProperties = LoadSortPropertiesFromQuery();
            service.SetupReport(report, criteria, sortProperties);
            return await GetReportContentAsync(report, fileType);
        }

        return NotFound();
    }
}
#nullable restore