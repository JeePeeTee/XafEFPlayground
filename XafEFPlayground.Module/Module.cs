using DevExpress.ExpressApp;
using DevExpress.ExpressApp.ReportsV2;
using DevExpress.ExpressApp.Updating;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl.EF.Kpi;
using XafEFPlayground.Module.BusinessObjects;

namespace XafEFPlayground.Module;

// For more typical usage scenarios, be sure to check out https://docs.devexpress.com/eXpressAppFramework/DevExpress.ExpressApp.ModuleBase.
public sealed class XafEFPlaygoundModule : ModuleBase {
    public XafEFPlaygoundModule() {
        // 
        // XafEFPlaygoundModule
        // 
        AdditionalExportedTypes.Add(typeof(ApplicationUser));
        AdditionalExportedTypes.Add(typeof(DevExpress.Persistent.BaseImpl.EF.PermissionPolicy.PermissionPolicyRole));
        AdditionalExportedTypes.Add(typeof(DevExpress.Persistent.BaseImpl.EF.ModelDifference));
        AdditionalExportedTypes.Add(typeof(DevExpress.Persistent.BaseImpl.EF.ModelDifferenceAspect));
        RequiredModuleTypes.Add(typeof(DevExpress.ExpressApp.SystemModule.SystemModule));
        RequiredModuleTypes.Add(typeof(DevExpress.ExpressApp.Security.SecurityModule));
        RequiredModuleTypes.Add(typeof(DevExpress.ExpressApp.AuditTrail.EFCore.AuditTrailModule));
        RequiredModuleTypes.Add(typeof(DevExpress.ExpressApp.Objects.BusinessClassLibraryCustomizationModule));
        RequiredModuleTypes.Add(typeof(DevExpress.ExpressApp.Chart.ChartModule));
        RequiredModuleTypes.Add(typeof(DevExpress.ExpressApp.ConditionalAppearance.ConditionalAppearanceModule));
        RequiredModuleTypes.Add(typeof(DevExpress.ExpressApp.Dashboards.DashboardsModule));
        RequiredModuleTypes.Add(typeof(DevExpress.ExpressApp.Kpi.KpiModule));
        RequiredModuleTypes.Add(typeof(DevExpress.ExpressApp.Notifications.NotificationsModule));
        RequiredModuleTypes.Add(typeof(DevExpress.ExpressApp.Office.OfficeModule));
        RequiredModuleTypes.Add(typeof(DevExpress.ExpressApp.PivotChart.PivotChartModuleBase));
        RequiredModuleTypes.Add(typeof(DevExpress.ExpressApp.PivotGrid.PivotGridModule));
        RequiredModuleTypes.Add(typeof(ReportsModuleV2));
        RequiredModuleTypes.Add(typeof(DevExpress.ExpressApp.Scheduler.SchedulerModuleBase));
        RequiredModuleTypes.Add(typeof(DevExpress.ExpressApp.Validation.ValidationModule));
        RequiredModuleTypes.Add(typeof(DevExpress.ExpressApp.ViewVariantsModule.ViewVariantsModule));
        DevExpress.ExpressApp.Kpi.KpiModule.UsedExportedTypes = UsedExportedTypes.Custom;
        DevExpress.ExpressApp.Security.SecurityModule.UsedExportedTypes = UsedExportedTypes.Custom;
        AdditionalExportedTypes.Add(typeof(DevExpress.Persistent.BaseImpl.EF.FileData));
        AdditionalExportedTypes.Add(typeof(DevExpress.Persistent.BaseImpl.EF.FileAttachment));
        AdditionalExportedTypes.Add(typeof(DevExpress.Persistent.BaseImpl.EF.Analysis));
        AdditionalExportedTypes.Add(typeof(DevExpress.Persistent.BaseImpl.EF.Event));
        AdditionalExportedTypes.Add(typeof(DevExpress.Persistent.BaseImpl.EF.Resource));
        AdditionalExportedTypes.Add(typeof(BaseKpiObject));
        AdditionalExportedTypes.Add(typeof(KpiDefinition));
        AdditionalExportedTypes.Add(typeof(KpiHistoryItem));
        AdditionalExportedTypes.Add(typeof(KpiInstance));
        AdditionalExportedTypes.Add(typeof(KpiScorecard));
    }

    public override IEnumerable<ModuleUpdater> GetModuleUpdaters(IObjectSpace objectSpace, Version versionFromDB) {
        ModuleUpdater updater = new DatabaseUpdate.Updater(objectSpace, versionFromDB);
        return new ModuleUpdater[] { updater };
    }

    public override void Setup(XafApplication application) {
        base.Setup(application);
        // Manage various aspects of the application UI and behavior at the module level.
    }

    public override void Setup(ApplicationModulesManager moduleManager) {
        base.Setup(moduleManager);
    }
}