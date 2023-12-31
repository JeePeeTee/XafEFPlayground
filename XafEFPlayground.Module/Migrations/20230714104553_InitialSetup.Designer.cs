﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using XafEFPlayground.Module.BusinessObjects;

#nullable disable

namespace XafEFPlayground.Module.Migrations
{
    [DbContext(typeof(XafEFPlaygoundEFCoreDbContext))]
    [Migration("20230714104553_InitialSetup")]
    partial class InitialSetup
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.3")
                .HasAnnotation("Proxies:ChangeTracking", true)
                .HasAnnotation("Proxies:CheckEquality", true)
                .HasAnnotation("Proxies:LazyLoading", false)
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("DevExpress.Persistent.BaseImpl.EF.Analysis", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<byte[]>("ChartSettingsContent")
                        .HasColumnType("varbinary(max)");

                    b.Property<string>("Criteria")
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("DimensionPropertiesString")
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("ObjectTypeName")
                        .HasColumnType("nvarchar(100)");

                    b.Property<byte[]>("PivotGridSettingsContent")
                        .HasColumnType("varbinary(max)");

                    b.HasKey("ID");

                    b.ToTable("Analysis");
                });

            modelBuilder.Entity("DevExpress.Persistent.BaseImpl.EF.DashboardData", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Content")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("SynchronizeTitle")
                        .HasColumnType("bit");

                    b.Property<string>("Title")
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("ID");

                    b.ToTable("DashboardData");
                });

            modelBuilder.Entity("DevExpress.Persistent.BaseImpl.EF.Event", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("AlarmTime")
                        .HasColumnType("datetime2");

                    b.Property<bool>("AllDay")
                        .HasColumnType("bit");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(100)");

                    b.Property<DateTime?>("EndOn")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsPostponed")
                        .HasColumnType("bit");

                    b.Property<int>("Label")
                        .HasColumnType("int");

                    b.Property<string>("Location")
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("RecurrenceInfoXml")
                        .HasMaxLength(300)
                        .HasColumnType("nvarchar(100)");

                    b.Property<Guid?>("RecurrencePatternID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("RemindInSeconds")
                        .HasColumnType("int");

                    b.Property<string>("ReminderInfoXml")
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(100)");

                    b.Property<DateTime?>("StartOn")
                        .HasColumnType("datetime2");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<string>("Subject")
                        .HasColumnType("nvarchar(100)");

                    b.Property<int>("Type")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.HasIndex("RecurrencePatternID");

                    b.ToTable("Event");
                });

            modelBuilder.Entity("DevExpress.Persistent.BaseImpl.EF.FileData", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<byte[]>("Content")
                        .HasColumnType("varbinary(max)");

                    b.Property<string>("FileName")
                        .HasColumnType("nvarchar(100)");

                    b.Property<int>("Size")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.ToTable("FileData");
                });

            modelBuilder.Entity("DevExpress.Persistent.BaseImpl.EF.Kpi.KpiDefinition", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("Active")
                        .HasColumnType("bit");

                    b.Property<DateTime>("ChangedOn")
                        .HasColumnType("datetime2");

                    b.Property<bool>("Compare")
                        .HasColumnType("bit");

                    b.Property<string>("Criteria")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Direction")
                        .HasColumnType("int");

                    b.Property<bool>("EnableCustomizeRepresentation")
                        .HasColumnType("bit");

                    b.Property<string>("Expression")
                        .HasColumnType("nvarchar(max)");

                    b.Property<float>("GreenZone")
                        .HasColumnType("real");

                    b.Property<int>("MeasurementFrequency")
                        .HasColumnType("int");

                    b.Property<int>("MeasurementMode")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("RangeName")
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("RangeToCompareName")
                        .HasColumnType("nvarchar(100)");

                    b.Property<float>("RedZone")
                        .HasColumnType("real");

                    b.Property<string>("SuppressedSeries")
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("TargetObjectTypeFullName")
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("ID");

                    b.ToTable("KpiDefinition");
                });

            modelBuilder.Entity("DevExpress.Persistent.BaseImpl.EF.Kpi.KpiHistoryItem", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("KpiInstanceID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("RangeEnd")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("RangeStart")
                        .HasColumnType("datetime2");

                    b.Property<float>("Value")
                        .HasColumnType("real");

                    b.HasKey("ID");

                    b.HasIndex("KpiInstanceID");

                    b.ToTable("KpiHistoryItem");
                });

            modelBuilder.Entity("DevExpress.Persistent.BaseImpl.EF.Kpi.KpiInstance", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("ForceMeasurementDateTime2")
                        .HasColumnType("datetime2")
                        .HasColumnName("ForceMeasurementDateTime");

                    b.Property<Guid>("KpiDefinitionID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Settings")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.HasIndex("KpiDefinitionID");

                    b.ToTable("KpiInstance");
                });

            modelBuilder.Entity("DevExpress.Persistent.BaseImpl.EF.Kpi.KpiScorecard", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("ID");

                    b.ToTable("KpiScorecard");
                });

            modelBuilder.Entity("DevExpress.Persistent.BaseImpl.EF.ModelDifference", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("ContextId")
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(100)");

                    b.Property<int>("Version")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.ToTable("ModelDifferences");
                });

            modelBuilder.Entity("DevExpress.Persistent.BaseImpl.EF.ModelDifferenceAspect", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(100)");

                    b.Property<Guid?>("OwnerID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Xml")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.HasIndex("OwnerID");

                    b.ToTable("ModelDifferenceAspects");
                });

            modelBuilder.Entity("DevExpress.Persistent.BaseImpl.EF.PermissionPolicy.PermissionPolicyActionPermissionObject", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("ActionId")
                        .HasColumnType("nvarchar(100)");

                    b.Property<Guid?>("RoleID")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("ID");

                    b.HasIndex("RoleID");

                    b.ToTable("PermissionPolicyActionPermissionObject");
                });

            modelBuilder.Entity("DevExpress.Persistent.BaseImpl.EF.PermissionPolicy.PermissionPolicyMemberPermissionsObject", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Criteria")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Members")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("ReadState")
                        .HasColumnType("int");

                    b.Property<Guid?>("TypePermissionObjectID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int?>("WriteState")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.HasIndex("TypePermissionObjectID");

                    b.ToTable("PermissionPolicyMemberPermissionsObject");
                });

            modelBuilder.Entity("DevExpress.Persistent.BaseImpl.EF.PermissionPolicy.PermissionPolicyNavigationPermissionObject", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("ItemPath")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("NavigateState")
                        .HasColumnType("int");

                    b.Property<Guid?>("RoleID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("TargetTypeFullName")
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("ID");

                    b.HasIndex("RoleID");

                    b.ToTable("PermissionPolicyNavigationPermissionObject");
                });

            modelBuilder.Entity("DevExpress.Persistent.BaseImpl.EF.PermissionPolicy.PermissionPolicyObjectPermissionsObject", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Criteria")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("DeleteState")
                        .HasColumnType("int");

                    b.Property<int?>("NavigateState")
                        .HasColumnType("int");

                    b.Property<int?>("ReadState")
                        .HasColumnType("int");

                    b.Property<Guid?>("TypePermissionObjectID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int?>("WriteState")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.HasIndex("TypePermissionObjectID");

                    b.ToTable("PermissionPolicyObjectPermissionsObject");
                });

            modelBuilder.Entity("DevExpress.Persistent.BaseImpl.EF.PermissionPolicy.PermissionPolicyRoleBase", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("CanEditModel")
                        .HasColumnType("bit");

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasColumnType("nvarchar(100)");

                    b.Property<bool>("IsAdministrative")
                        .HasColumnType("bit");

                    b.Property<bool>("IsAllowPermissionPriority")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(100)");

                    b.Property<int>("PermissionPolicy")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.ToTable("PermissionPolicyRoleBase");

                    b.HasDiscriminator<string>("Discriminator").HasValue("PermissionPolicyRoleBase");

                    b.UseTphMappingStrategy();
                });

            modelBuilder.Entity("DevExpress.Persistent.BaseImpl.EF.PermissionPolicy.PermissionPolicyTypePermissionObject", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int?>("CreateState")
                        .HasColumnType("int");

                    b.Property<int?>("DeleteState")
                        .HasColumnType("int");

                    b.Property<int?>("NavigateState")
                        .HasColumnType("int");

                    b.Property<int?>("ReadState")
                        .HasColumnType("int");

                    b.Property<Guid?>("RoleID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("TargetTypeFullName")
                        .HasColumnType("nvarchar(100)");

                    b.Property<int?>("WriteState")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.HasIndex("RoleID");

                    b.ToTable("PermissionPolicyTypePermissionObject");
                });

            modelBuilder.Entity("DevExpress.Persistent.BaseImpl.EF.PermissionPolicy.PermissionPolicyUser", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("ChangePasswordOnFirstLogon")
                        .HasColumnType("bit");

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasColumnType("nvarchar(100)");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<string>("StoredPassword")
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("UserName")
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("ID");

                    b.ToTable("PermissionPolicyUser");

                    b.HasDiscriminator<string>("Discriminator").HasValue("PermissionPolicyUser");

                    b.UseTphMappingStrategy();
                });

            modelBuilder.Entity("DevExpress.Persistent.BaseImpl.EF.ReportDataV2", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<byte[]>("Content")
                        .HasColumnType("varbinary(max)");

                    b.Property<string>("DataTypeName")
                        .HasColumnType("nvarchar(512)");

                    b.Property<string>("DisplayName")
                        .HasColumnType("nvarchar(100)");

                    b.Property<bool>("IsInplaceReport")
                        .HasColumnType("bit");

                    b.Property<string>("ParametersObjectTypeName")
                        .HasColumnType("nvarchar(512)");

                    b.Property<string>("PredefinedReportTypeName")
                        .HasColumnType("nvarchar(512)");

                    b.HasKey("ID");

                    b.ToTable("ReportDataV2");
                });

            modelBuilder.Entity("DevExpress.Persistent.BaseImpl.EF.Resource", b =>
                {
                    b.Property<Guid>("Key")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Caption")
                        .HasColumnType("nvarchar(100)");

                    b.Property<int>("Color_Int")
                        .HasColumnType("int");

                    b.HasKey("Key");

                    b.ToTable("Resource");
                });

            modelBuilder.Entity("DevExpress.Persistent.BaseImpl.EFCore.AuditTrail.AuditDataItemPersistent", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("AuditedObjectID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(2048)");

                    b.Property<DateTime>("ModifiedOn")
                        .HasColumnType("datetime2");

                    b.Property<Guid?>("NewObjectID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("NewValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("OldObjectID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("OldValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("OperationType")
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("PropertyName")
                        .HasColumnType("nvarchar(100)");

                    b.Property<Guid?>("UserObjectID")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("ID");

                    b.HasIndex("AuditedObjectID");

                    b.HasIndex("NewObjectID");

                    b.HasIndex("OldObjectID");

                    b.HasIndex("UserObjectID");

                    b.ToTable("AuditData");
                });

            modelBuilder.Entity("DevExpress.Persistent.BaseImpl.EFCore.AuditTrail.AuditEFCoreWeakReference", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("DefaultString")
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Key")
                        .HasColumnType("nvarchar(100)");

                    b.Property<DateTime>("LastModifiedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("TypeName")
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("ID");

                    b.HasIndex("Key", "TypeName");

                    b.ToTable("AuditEFCoreWeakReference");
                });

            modelBuilder.Entity("EventResource", b =>
                {
                    b.Property<Guid>("EventsID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("ResourcesKey")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("EventsID", "ResourcesKey");

                    b.HasIndex("ResourcesKey");

                    b.ToTable("EventResource");
                });

            modelBuilder.Entity("KpiInstanceKpiScorecard", b =>
                {
                    b.Property<Guid>("IndicatorsID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("ScorecardsID")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("IndicatorsID", "ScorecardsID");

                    b.HasIndex("ScorecardsID");

                    b.ToTable("KpiInstanceKpiScorecard");
                });

            modelBuilder.Entity("PermissionPolicyRolePermissionPolicyUser", b =>
                {
                    b.Property<Guid>("RolesID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("UsersID")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("RolesID", "UsersID");

                    b.HasIndex("UsersID");

                    b.ToTable("PermissionPolicyRolePermissionPolicyUser");
                });

            modelBuilder.Entity("XafEFPlayground.Module.BusinessObjects.ApplicationUserLoginInfo", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("LoginProviderName")
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("ProviderUserKey")
                        .HasColumnType("nvarchar(100)");

                    b.Property<Guid>("UserForeignKey")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("ID");

                    b.HasIndex("UserForeignKey");

                    b.HasIndex("LoginProviderName", "ProviderUserKey")
                        .IsUnique()
                        .HasFilter("[LoginProviderName] IS NOT NULL AND [ProviderUserKey] IS NOT NULL");

                    b.ToTable("PermissionPolicyUserLoginInfo");
                });

            modelBuilder.Entity("XafEFPlayground.Module.BusinessObjects.Book", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<decimal>("BasePrice")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("money")
                        .HasDefaultValue(0m);

                    b.Property<string>("BookTitle")
                        .IsRequired()
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(100)");

                    b.Property<DateTime?>("Created")
                        .IsRequired()
                        .HasColumnType("datetime2")
                        .HasColumnName("Created");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(100)")
                        .HasColumnName("CreatedBy");

                    b.Property<DateTime?>("Deleted")
                        .HasColumnType("datetime2")
                        .HasColumnName("Deleted");

                    b.Property<string>("DeletedBy")
                        .HasColumnType("nvarchar(100)")
                        .HasColumnName("DeletedBy");

                    b.Property<bool>("IsDeleted")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(false)
                        .HasColumnName("IsDeleted");

                    b.Property<DateTime?>("LastModified")
                        .HasColumnType("datetime2")
                        .HasColumnName("LastModified");

                    b.Property<string>("LastModifiedBy")
                        .HasColumnType("nvarchar(100)")
                        .HasColumnName("LastModifiedBy");

                    b.Property<DateTime?>("PublishDate")
                        .HasColumnType("datetime2");

                    b.Property<byte[]>("Version")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("rowversion")
                        .HasColumnName("Version");

                    b.HasKey("ID");

                    b.HasIndex("BookTitle")
                        .IsUnique();

                    b.ToTable("MyAppBook", (string)null);
                });

            modelBuilder.Entity("DevExpress.Persistent.BaseImpl.EF.PermissionPolicy.PermissionPolicyRole", b =>
                {
                    b.HasBaseType("DevExpress.Persistent.BaseImpl.EF.PermissionPolicy.PermissionPolicyRoleBase");

                    b.HasDiscriminator().HasValue("PermissionPolicyRole");
                });

            modelBuilder.Entity("XafEFPlayground.Module.BusinessObjects.ApplicationUser", b =>
                {
                    b.HasBaseType("DevExpress.Persistent.BaseImpl.EF.PermissionPolicy.PermissionPolicyUser");

                    b.HasDiscriminator().HasValue("ApplicationUser");
                });

            modelBuilder.Entity("DevExpress.Persistent.BaseImpl.EF.Event", b =>
                {
                    b.HasOne("DevExpress.Persistent.BaseImpl.EF.Event", "RecurrencePattern")
                        .WithMany("RecurrenceEvents")
                        .HasForeignKey("RecurrencePatternID");

                    b.Navigation("RecurrencePattern");
                });

            modelBuilder.Entity("DevExpress.Persistent.BaseImpl.EF.Kpi.KpiHistoryItem", b =>
                {
                    b.HasOne("DevExpress.Persistent.BaseImpl.EF.Kpi.KpiInstance", "KpiInstance")
                        .WithMany("HistoryItems")
                        .HasForeignKey("KpiInstanceID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("KpiInstance");
                });

            modelBuilder.Entity("DevExpress.Persistent.BaseImpl.EF.Kpi.KpiInstance", b =>
                {
                    b.HasOne("DevExpress.Persistent.BaseImpl.EF.Kpi.KpiDefinition", "KpiDefinition")
                        .WithMany("KpiInstances")
                        .HasForeignKey("KpiDefinitionID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("KpiDefinition");
                });

            modelBuilder.Entity("DevExpress.Persistent.BaseImpl.EF.ModelDifferenceAspect", b =>
                {
                    b.HasOne("DevExpress.Persistent.BaseImpl.EF.ModelDifference", "Owner")
                        .WithMany("Aspects")
                        .HasForeignKey("OwnerID")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.Navigation("Owner");
                });

            modelBuilder.Entity("DevExpress.Persistent.BaseImpl.EF.PermissionPolicy.PermissionPolicyActionPermissionObject", b =>
                {
                    b.HasOne("DevExpress.Persistent.BaseImpl.EF.PermissionPolicy.PermissionPolicyRoleBase", "Role")
                        .WithMany("ActionPermissions")
                        .HasForeignKey("RoleID");

                    b.Navigation("Role");
                });

            modelBuilder.Entity("DevExpress.Persistent.BaseImpl.EF.PermissionPolicy.PermissionPolicyMemberPermissionsObject", b =>
                {
                    b.HasOne("DevExpress.Persistent.BaseImpl.EF.PermissionPolicy.PermissionPolicyTypePermissionObject", "TypePermissionObject")
                        .WithMany("MemberPermissions")
                        .HasForeignKey("TypePermissionObjectID");

                    b.Navigation("TypePermissionObject");
                });

            modelBuilder.Entity("DevExpress.Persistent.BaseImpl.EF.PermissionPolicy.PermissionPolicyNavigationPermissionObject", b =>
                {
                    b.HasOne("DevExpress.Persistent.BaseImpl.EF.PermissionPolicy.PermissionPolicyRoleBase", "Role")
                        .WithMany("NavigationPermissions")
                        .HasForeignKey("RoleID");

                    b.Navigation("Role");
                });

            modelBuilder.Entity("DevExpress.Persistent.BaseImpl.EF.PermissionPolicy.PermissionPolicyObjectPermissionsObject", b =>
                {
                    b.HasOne("DevExpress.Persistent.BaseImpl.EF.PermissionPolicy.PermissionPolicyTypePermissionObject", "TypePermissionObject")
                        .WithMany("ObjectPermissions")
                        .HasForeignKey("TypePermissionObjectID");

                    b.Navigation("TypePermissionObject");
                });

            modelBuilder.Entity("DevExpress.Persistent.BaseImpl.EF.PermissionPolicy.PermissionPolicyTypePermissionObject", b =>
                {
                    b.HasOne("DevExpress.Persistent.BaseImpl.EF.PermissionPolicy.PermissionPolicyRoleBase", "Role")
                        .WithMany("TypePermissions")
                        .HasForeignKey("RoleID");

                    b.Navigation("Role");
                });

            modelBuilder.Entity("DevExpress.Persistent.BaseImpl.EFCore.AuditTrail.AuditDataItemPersistent", b =>
                {
                    b.HasOne("DevExpress.Persistent.BaseImpl.EFCore.AuditTrail.AuditEFCoreWeakReference", "AuditedObject")
                        .WithMany("AuditItems")
                        .HasForeignKey("AuditedObjectID");

                    b.HasOne("DevExpress.Persistent.BaseImpl.EFCore.AuditTrail.AuditEFCoreWeakReference", "NewObject")
                        .WithMany("NewItems")
                        .HasForeignKey("NewObjectID");

                    b.HasOne("DevExpress.Persistent.BaseImpl.EFCore.AuditTrail.AuditEFCoreWeakReference", "OldObject")
                        .WithMany("OldItems")
                        .HasForeignKey("OldObjectID");

                    b.HasOne("DevExpress.Persistent.BaseImpl.EFCore.AuditTrail.AuditEFCoreWeakReference", "UserObject")
                        .WithMany("UserItems")
                        .HasForeignKey("UserObjectID");

                    b.Navigation("AuditedObject");

                    b.Navigation("NewObject");

                    b.Navigation("OldObject");

                    b.Navigation("UserObject");
                });

            modelBuilder.Entity("EventResource", b =>
                {
                    b.HasOne("DevExpress.Persistent.BaseImpl.EF.Event", null)
                        .WithMany()
                        .HasForeignKey("EventsID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DevExpress.Persistent.BaseImpl.EF.Resource", null)
                        .WithMany()
                        .HasForeignKey("ResourcesKey")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("KpiInstanceKpiScorecard", b =>
                {
                    b.HasOne("DevExpress.Persistent.BaseImpl.EF.Kpi.KpiInstance", null)
                        .WithMany()
                        .HasForeignKey("IndicatorsID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DevExpress.Persistent.BaseImpl.EF.Kpi.KpiScorecard", null)
                        .WithMany()
                        .HasForeignKey("ScorecardsID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("PermissionPolicyRolePermissionPolicyUser", b =>
                {
                    b.HasOne("DevExpress.Persistent.BaseImpl.EF.PermissionPolicy.PermissionPolicyRole", null)
                        .WithMany()
                        .HasForeignKey("RolesID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DevExpress.Persistent.BaseImpl.EF.PermissionPolicy.PermissionPolicyUser", null)
                        .WithMany()
                        .HasForeignKey("UsersID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("XafEFPlayground.Module.BusinessObjects.ApplicationUserLoginInfo", b =>
                {
                    b.HasOne("XafEFPlayground.Module.BusinessObjects.ApplicationUser", "User")
                        .WithMany("UserLogins")
                        .HasForeignKey("UserForeignKey")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("DevExpress.Persistent.BaseImpl.EF.Event", b =>
                {
                    b.Navigation("RecurrenceEvents");
                });

            modelBuilder.Entity("DevExpress.Persistent.BaseImpl.EF.Kpi.KpiDefinition", b =>
                {
                    b.Navigation("KpiInstances");
                });

            modelBuilder.Entity("DevExpress.Persistent.BaseImpl.EF.Kpi.KpiInstance", b =>
                {
                    b.Navigation("HistoryItems");
                });

            modelBuilder.Entity("DevExpress.Persistent.BaseImpl.EF.ModelDifference", b =>
                {
                    b.Navigation("Aspects");
                });

            modelBuilder.Entity("DevExpress.Persistent.BaseImpl.EF.PermissionPolicy.PermissionPolicyRoleBase", b =>
                {
                    b.Navigation("ActionPermissions");

                    b.Navigation("NavigationPermissions");

                    b.Navigation("TypePermissions");
                });

            modelBuilder.Entity("DevExpress.Persistent.BaseImpl.EF.PermissionPolicy.PermissionPolicyTypePermissionObject", b =>
                {
                    b.Navigation("MemberPermissions");

                    b.Navigation("ObjectPermissions");
                });

            modelBuilder.Entity("DevExpress.Persistent.BaseImpl.EFCore.AuditTrail.AuditEFCoreWeakReference", b =>
                {
                    b.Navigation("AuditItems");

                    b.Navigation("NewItems");

                    b.Navigation("OldItems");

                    b.Navigation("UserItems");
                });

            modelBuilder.Entity("XafEFPlayground.Module.BusinessObjects.ApplicationUser", b =>
                {
                    b.Navigation("UserLogins");
                });
#pragma warning restore 612, 618
        }
    }
}
