namespace ERP_API.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class ecomContext : DbContext
    {
        public ecomContext()
            : base("name=ErpDbContext")
        {
        }

        public virtual DbSet<Account_Contract> Account_Contract { get; set; }
        public virtual DbSet<Account_ContractLease> Account_ContractLease { get; set; }
        public virtual DbSet<Account_ContractSell> Account_ContractSell { get; set; }
        public virtual DbSet<Account_InfoPartyA> Account_InfoPartyA { get; set; }
        public virtual DbSet<Account_MemberCard> Account_MemberCard { get; set; }
        public virtual DbSet<Account_MemberCardDetail> Account_MemberCardDetail { get; set; }
        public virtual DbSet<Account_Payment> Account_Payment { get; set; }
        public virtual DbSet<Account_PaymentDetail> Account_PaymentDetail { get; set; }
        public virtual DbSet<Account_ProcessPayment> Account_ProcessPayment { get; set; }
        public virtual DbSet<Account_Receipt> Account_Receipt { get; set; }
        public virtual DbSet<Account_ReceiptDetail> Account_ReceiptDetail { get; set; }
        public virtual DbSet<Account_Transaction> Account_Transaction { get; set; }
        public virtual DbSet<Account_TransactionLiabilities> Account_TransactionLiabilities { get; set; }
        public virtual DbSet<Account_TransactionRelationship> Account_TransactionRelationship { get; set; }
        public virtual DbSet<Crm_Answer> Crm_Answer { get; set; }
        public virtual DbSet<Crm_Campaign> Crm_Campaign { get; set; }
        public virtual DbSet<Crm_EmailLog> Crm_EmailLog { get; set; }
        public virtual DbSet<Crm_LogAccumulatePoint> Crm_LogAccumulatePoint { get; set; }
        public virtual DbSet<Crm_Process> Crm_Process { get; set; }
        public virtual DbSet<Crm_ProcessAction> Crm_ProcessAction { get; set; }
        public virtual DbSet<Crm_ProcessApplied> Crm_ProcessApplied { get; set; }
        public virtual DbSet<Crm_ProcessStage> Crm_ProcessStage { get; set; }
        public virtual DbSet<Crm_ProcessStep> Crm_ProcessStep { get; set; }
        public virtual DbSet<Crm_Question> Crm_Question { get; set; }
        public virtual DbSet<Crm_SMSLog> Crm_SMSLog { get; set; }
        public virtual DbSet<Crm_Task> Crm_Task { get; set; }
        public virtual DbSet<Crm_Vote> Crm_Vote { get; set; }
        public virtual DbSet<RE_Block> RE_Block { get; set; }
        public virtual DbSet<RE_Condos> RE_Condos { get; set; }
        public virtual DbSet<RE_CondosLayout> RE_CondosLayout { get; set; }
        public virtual DbSet<RE_CondosPrice> RE_CondosPrice { get; set; }
        public virtual DbSet<RE_Floor> RE_Floor { get; set; }
        public virtual DbSet<RE_Project> RE_Project { get; set; }
        public virtual DbSet<Sale_Commision> Sale_Commision { get; set; }
        public virtual DbSet<Sale_Commision_Customer> Sale_Commision_Customer { get; set; }
        public virtual DbSet<Sale_Commision_Staff> Sale_Commision_Staff { get; set; }
        public virtual DbSet<Sale_CommissionCus> Sale_CommissionCus { get; set; }
        public virtual DbSet<Sale_Contact> Sale_Contact { get; set; }
        public virtual DbSet<Sale_Customer> Sale_Customer { get; set; }
        public virtual DbSet<Sale_CustomerCommitment> Sale_CustomerCommitment { get; set; }
        public virtual DbSet<Sale_CustomerDiscount> Sale_CustomerDiscount { get; set; }
        public virtual DbSet<Sale_Inventory> Sale_Inventory { get; set; }
        public virtual DbSet<Sale_InventoryByMonth> Sale_InventoryByMonth { get; set; }
        public virtual DbSet<Sale_LogLoyaltyPoint> Sale_LogLoyaltyPoint { get; set; }
        public virtual DbSet<Sale_LogServiceRemminder> Sale_LogServiceRemminder { get; set; }
        public virtual DbSet<Sale_LoyaltyPoint> Sale_LoyaltyPoint { get; set; }
        public virtual DbSet<Sale_Membership> Sale_Membership { get; set; }
        public virtual DbSet<Sale_ObjectAttribute> Sale_ObjectAttribute { get; set; }
        public virtual DbSet<Sale_ObjectAttributeValue> Sale_ObjectAttributeValue { get; set; }
        public virtual DbSet<Sale_PhysicalInventory> Sale_PhysicalInventory { get; set; }
        public virtual DbSet<Sale_PhysicalInventoryDetail> Sale_PhysicalInventoryDetail { get; set; }
        public virtual DbSet<Sale_Product> Sale_Product { get; set; }
        public virtual DbSet<Sale_Product_SKU> Sale_Product_SKU { get; set; }
        public virtual DbSet<Sale_ProductDamaged> Sale_ProductDamaged { get; set; }
        public virtual DbSet<Sale_ProductInbound> Sale_ProductInbound { get; set; }
        public virtual DbSet<Sale_ProductInboundDetail> Sale_ProductInboundDetail { get; set; }
        public virtual DbSet<Sale_ProductInvoice> Sale_ProductInvoice { get; set; }
        public virtual DbSet<Sale_ProductInvoiceDetail> Sale_ProductInvoiceDetail { get; set; }
        public virtual DbSet<Sale_ProductOutbound> Sale_ProductOutbound { get; set; }
        public virtual DbSet<Sale_ProductOutboundDetail> Sale_ProductOutboundDetail { get; set; }
        public virtual DbSet<Sale_Promotion> Sale_Promotion { get; set; }
        public virtual DbSet<Sale_PromotionDetail> Sale_PromotionDetail { get; set; }
        public virtual DbSet<Sale_PurchaseOrder> Sale_PurchaseOrder { get; set; }
        public virtual DbSet<Sale_PurchaseOrderDetail> Sale_PurchaseOrderDetail { get; set; }
        public virtual DbSet<Sale_RequestInbound> Sale_RequestInbound { get; set; }
        public virtual DbSet<Sale_RequestInboundDetail> Sale_RequestInboundDetail { get; set; }
        public virtual DbSet<Sale_SalesReturns> Sale_SalesReturns { get; set; }
        public virtual DbSet<Sale_SalesReturnsDetail> Sale_SalesReturnsDetail { get; set; }
        public virtual DbSet<Sale_Service> Sale_Service { get; set; }
        public virtual DbSet<Sale_ServiceCombo> Sale_ServiceCombo { get; set; }
        public virtual DbSet<Sale_ServiceInvoice> Sale_ServiceInvoice { get; set; }
        public virtual DbSet<Sale_ServiceInvoiceDetail> Sale_ServiceInvoiceDetail { get; set; }
        public virtual DbSet<Sale_ServiceReminder> Sale_ServiceReminder { get; set; }
        public virtual DbSet<Sale_ServiceReminderGroup> Sale_ServiceReminderGroup { get; set; }
        public virtual DbSet<Sale_ServiceSchedule> Sale_ServiceSchedule { get; set; }
        public virtual DbSet<Sale_Supplier> Sale_Supplier { get; set; }
        public virtual DbSet<Sale_TotalDiscountMoneyNT> Sale_TotalDiscountMoneyNT { get; set; }
        public virtual DbSet<Sale_UsingServiceLog> Sale_UsingServiceLog { get; set; }
        public virtual DbSet<Sale_UsingServiceLogDetail> Sale_UsingServiceLogDetail { get; set; }
        public virtual DbSet<Sale_Warehouse> Sale_Warehouse { get; set; }
        public virtual DbSet<Sale_WarehouseLocationItem> Sale_WarehouseLocationItem { get; set; }
        public virtual DbSet<Staff_Allowance> Staff_Allowance { get; set; }
        public virtual DbSet<Staff_Bank> Staff_Bank { get; set; }
        public virtual DbSet<Staff_BonusDiscipline> Staff_BonusDiscipline { get; set; }
        public virtual DbSet<Staff_Branch> Staff_Branch { get; set; }
        public virtual DbSet<Staff_BranchDepartment> Staff_BranchDepartment { get; set; }
        public virtual DbSet<Staff_CalendarVisitDrugStore> Staff_CalendarVisitDrugStore { get; set; }
        public virtual DbSet<Staff_CheckInOut> Staff_CheckInOut { get; set; }
        public virtual DbSet<Staff_DayOff> Staff_DayOff { get; set; }
        public virtual DbSet<Staff_DocumentAttribute> Staff_DocumentAttribute { get; set; }
        public virtual DbSet<Staff_DocumentField> Staff_DocumentField { get; set; }
        public virtual DbSet<Staff_DocumentType> Staff_DocumentType { get; set; }
        public virtual DbSet<Staff_DotBCBHXH> Staff_DotBCBHXH { get; set; }
        public virtual DbSet<Staff_DotBCBHXHDetail> Staff_DotBCBHXHDetail { get; set; }
        public virtual DbSet<Staff_DotGQCDBHXH> Staff_DotGQCDBHXH { get; set; }
        public virtual DbSet<Staff_DotGQCDBHXHDetail> Staff_DotGQCDBHXHDetail { get; set; }
        public virtual DbSet<Staff_FingerPrint> Staff_FingerPrint { get; set; }
        public virtual DbSet<Staff_FPMachine> Staff_FPMachine { get; set; }
        public virtual DbSet<Staff_GiamTruThueTNCN> Staff_GiamTruThueTNCN { get; set; }
        public virtual DbSet<Staff_HistoryCommissionStaff> Staff_HistoryCommissionStaff { get; set; }
        public virtual DbSet<Staff_Holidays> Staff_Holidays { get; set; }
        public virtual DbSet<Staff_InternalNotifications> Staff_InternalNotifications { get; set; }
        public virtual DbSet<Staff_KPICatalog> Staff_KPICatalog { get; set; }
        public virtual DbSet<Staff_KPIItem> Staff_KPIItem { get; set; }
        public virtual DbSet<Staff_KPILogDetail> Staff_KPILogDetail { get; set; }
        public virtual DbSet<Staff_KPILogDetail_Item> Staff_KPILogDetail_Item { get; set; }
        public virtual DbSet<Staff_LabourContract> Staff_LabourContract { get; set; }
        public virtual DbSet<Staff_LabourContractType> Staff_LabourContractType { get; set; }
        public virtual DbSet<Staff_LogDocumentAttribute> Staff_LogDocumentAttribute { get; set; }
        public virtual DbSet<Staff_LuongGiuHopDong> Staff_LuongGiuHopDong { get; set; }
        public virtual DbSet<Staff_NotificationsDetail> Staff_NotificationsDetail { get; set; }
        public virtual DbSet<Staff_Position> Staff_Position { get; set; }
        public virtual DbSet<Staff_ProcessPay> Staff_ProcessPay { get; set; }
        public virtual DbSet<Staff_RegisterForOvertime> Staff_RegisterForOvertime { get; set; }
        public virtual DbSet<Staff_SalaryAdvance> Staff_SalaryAdvance { get; set; }
        public virtual DbSet<Staff_SalarySetting> Staff_SalarySetting { get; set; }
        public virtual DbSet<Staff_SalarySettingDetail> Staff_SalarySettingDetail { get; set; }
        public virtual DbSet<Staff_SalarySettingDetail_Staff> Staff_SalarySettingDetail_Staff { get; set; }
        public virtual DbSet<Staff_SalaryTable> Staff_SalaryTable { get; set; }
        public virtual DbSet<Staff_SalaryTableDetail> Staff_SalaryTableDetail { get; set; }
        public virtual DbSet<Staff_SalaryTableDetail_Report> Staff_SalaryTableDetail_Report { get; set; }
        public virtual DbSet<Staff_SalaryTableDetail_Staff> Staff_SalaryTableDetail_Staff { get; set; }
        public virtual DbSet<Staff_Shifts> Staff_Shifts { get; set; }
        public virtual DbSet<Staff_SocialInsurance> Staff_SocialInsurance { get; set; }
        public virtual DbSet<Staff_Staff> Staff_Staff { get; set; }
        public virtual DbSet<Staff_StaffFamily> Staff_StaffFamily { get; set; }
        public virtual DbSet<Staff_SymbolTimekeeping> Staff_SymbolTimekeeping { get; set; }
        public virtual DbSet<Staff_Tax> Staff_Tax { get; set; }
        public virtual DbSet<Staff_TaxIncomePerson> Staff_TaxIncomePerson { get; set; }
        public virtual DbSet<Staff_TaxIncomePersonDetail> Staff_TaxIncomePersonDetail { get; set; }
        public virtual DbSet<Staff_TaxRate> Staff_TaxRate { get; set; }
        public virtual DbSet<Staff_Technique> Staff_Technique { get; set; }
        public virtual DbSet<Staff_ThuNhapChiuThue> Staff_ThuNhapChiuThue { get; set; }
        public virtual DbSet<Staff_Timekeeping> Staff_Timekeeping { get; set; }
        public virtual DbSet<Staff_TimekeepingList> Staff_TimekeepingList { get; set; }
        public virtual DbSet<Staff_TimekeepingSynthesis> Staff_TimekeepingSynthesis { get; set; }
        public virtual DbSet<Staff_TransferWork> Staff_TransferWork { get; set; }
        public virtual DbSet<Staff_WelfarePrograms> Staff_WelfarePrograms { get; set; }
        public virtual DbSet<Staff_WelfareProgramsDetail> Staff_WelfareProgramsDetail { get; set; }
        public virtual DbSet<Staff_WorkingProcess> Staff_WorkingProcess { get; set; }
        public virtual DbSet<Staff_WorkSchedules> Staff_WorkSchedules { get; set; }
        public virtual DbSet<sysdiagram> sysdiagrams { get; set; }
        public virtual DbSet<System_BOLog> System_BOLog { get; set; }
        public virtual DbSet<System_Category> System_Category { get; set; }
        public virtual DbSet<System_Language> System_Language { get; set; }
        public virtual DbSet<System_MetadataField> System_MetadataField { get; set; }
        public virtual DbSet<System_Module> System_Module { get; set; }
        public virtual DbSet<System_ModuleRelationship> System_ModuleRelationship { get; set; }
        public virtual DbSet<System_News> System_News { get; set; }
        public virtual DbSet<System_Page> System_Page { get; set; }
        public virtual DbSet<System_PageMenu> System_PageMenu { get; set; }
        public virtual DbSet<System_Setting> System_Setting { get; set; }
        public virtual DbSet<System_TemplatePrint> System_TemplatePrint { get; set; }
        public virtual DbSet<System_User> System_User { get; set; }
        public virtual DbSet<System_UserSetting> System_UserSetting { get; set; }
        public virtual DbSet<System_UserType> System_UserType { get; set; }
        public virtual DbSet<Ward> Wards { get; set; }
        public virtual DbSet<webpages_Membership> webpages_Membership { get; set; }
        public virtual DbSet<webpages_OAuthMembership> webpages_OAuthMembership { get; set; }
        public virtual DbSet<webpages_Roles> webpages_Roles { get; set; }
        public virtual DbSet<Staff_HealthInsurance> Staff_HealthInsurance { get; set; }
        public virtual DbSet<Staff_KPILog> Staff_KPILog { get; set; }
        public virtual DbSet<System_Loc_District> System_Loc_District { get; set; }
        public virtual DbSet<System_Loc_Province> System_Loc_Province { get; set; }
        public virtual DbSet<System_Loc_Ward> System_Loc_Ward { get; set; }
        public virtual DbSet<System_LoginLog> System_LoginLog { get; set; }
        public virtual DbSet<System_News_ViewedUser> System_News_ViewedUser { get; set; }
        public virtual DbSet<System_UserPage> System_UserPage { get; set; }
        public virtual DbSet<System_UserTypePage> System_UserTypePage { get; set; }
        public virtual DbSet<Page_New> Page_New { get; set; }
        public virtual DbSet<Page_Setup> Page_Setup { get; set; }
        public virtual DbSet<Page_Slide> Page_Slide { get; set; }
        public virtual DbSet<Page_ViewProductUser> Page_ViewProductUser { get; set; }
        public virtual DbSet<Page_CategoryPost> Page_CategoryPost { get; set; }
        public virtual DbSet<Page_Comment> Page_Comment { get; set; }
        public virtual DbSet<DM_BAIVE_YHL> DM_BAIVE_YHL { get; set; }
        public virtual DbSet<DM_BANNER_SLIDER> DM_BANNER_SLIDER { get; set; }
        public virtual DbSet<DM_BAOCHI> DM_BAOCHI { get; set; }
        public virtual DbSet<DM_BEST_SELLER> DM_BEST_SELLER { get; set; }
        public virtual DbSet<DM_CAMNHAN_KHANG> DM_CAMNHAN_KHANG { get; set; }
        public virtual DbSet<DM_CHOP_DEAL> DM_CHOP_DEAL { get; set; }
        public virtual DbSet<DM_DEALHOT> DM_DEALHOT { get; set; }
        public virtual DbSet<DM_LOAISANPHAM> DM_LOAISANPHAM { get; set; }
        public virtual DbSet<DM_NGHESY_TINDUNG> DM_NGHESY_TINDUNG { get; set; }
        public virtual DbSet<DM_NHOMSANPHAM> DM_NHOMSANPHAM { get; set; }
        public virtual DbSet<DM_NHOMTIN> DM_NHOMTIN { get; set; }
        public virtual DbSet<DM_TIN_KHUYENMAI> DM_TIN_KHUYENMAI { get; set; }
        public virtual DbSet<DM_TINTUC> DM_TINTUC { get; set; }
        public virtual DbSet<Sale_Product_samegroup> Sale_Product_samegroup { get; set; }
        public virtual DbSet<Sale_Product_samesize> Sale_Product_samesize { get; set; }
        public virtual DbSet<DM_LOAIBAI> DM_LOAIBAI { get; set; }
        public virtual DbSet<vwProduct_PromotionNew> vwProduct_Promotion { get; set; }
        public virtual DbSet<Feedback> Feedbacks { get; set; }

        public virtual DbSet<DM_TINTUC_tags> DM_TINTUC_tags { get; set; }
        public virtual DbSet<DM_TINTUC_tags_list> DM_TINTUC_tags_list { get; set; }
        public virtual DbSet<Sale_Commision_Apply> Sale_Commision_Apply { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DM_BAIVE_YHL>()
                .Property(e => e.CODE_LOAIBAI)
                .IsUnicode(false);

            modelBuilder.Entity<DM_BAIVE_YHL>()
                .Property(e => e.lat)
                .IsUnicode(false);

            modelBuilder.Entity<DM_BAIVE_YHL>()
                .Property(e => e.lng)
                .IsUnicode(false);

            modelBuilder.Entity<DM_LOAIBAI>()
            .Property(e => e.CODE_LOAIBAI)
            .IsUnicode(false);
            modelBuilder.Entity<DM_CHOP_DEAL>()
                .Property(e => e.DON_GIA)
                .HasPrecision(18, 0);

            modelBuilder.Entity<DM_CHOP_DEAL>()
                .Property(e => e.THANH_TIEN)
                .HasPrecision(18, 0);

            modelBuilder.Entity<DM_CHOP_DEAL>()
                .Property(e => e.EMAIL)
                .IsUnicode(false);

            modelBuilder.Entity<DM_CHOP_DEAL>()
                .Property(e => e.DIENTHOAI)
                .IsUnicode(false);



            modelBuilder.Entity<Account_Contract>()
                .Property(e => e.IdTypeContract)
                .IsUnicode(false);

            modelBuilder.Entity<Account_Contract>()
                .Property(e => e.TransactionCode)
                .IsUnicode(false);

            modelBuilder.Entity<Account_Payment>()
                .Property(e => e.Code)
                .IsUnicode(false);

            modelBuilder.Entity<Account_Payment>()
                .Property(e => e.Amount)
                .HasPrecision(18, 0);

            modelBuilder.Entity<Account_Payment>()
                .Property(e => e.BankAccountNo)
                .IsUnicode(false);

            modelBuilder.Entity<Account_Payment>()
                .Property(e => e.MaChungTuGoc)
                .IsUnicode(false);

            modelBuilder.Entity<Account_Payment>()
                .Property(e => e.LoaiChungTuGoc)
                .IsUnicode(false);

            modelBuilder.Entity<Account_PaymentDetail>()
                .Property(e => e.MaChungTuGoc)
                .IsUnicode(false);

            modelBuilder.Entity<Account_PaymentDetail>()
                .Property(e => e.LoaiChungTuGoc)
                .IsUnicode(false);

            modelBuilder.Entity<Account_PaymentDetail>()
                .Property(e => e.Amount)
                .HasPrecision(18, 0);

            modelBuilder.Entity<Account_ProcessPayment>()
                .Property(e => e.TransactionCode)
                .IsUnicode(false);

            modelBuilder.Entity<Account_Receipt>()
                .Property(e => e.Code)
                .IsUnicode(false);

            modelBuilder.Entity<Account_Receipt>()
                .Property(e => e.Amount)
                .HasPrecision(18, 0);

            modelBuilder.Entity<Account_Receipt>()
                .Property(e => e.BankAccountNo)
                .IsUnicode(false);

            modelBuilder.Entity<Account_Receipt>()
                .Property(e => e.MaChungTuGoc)
                .IsUnicode(false);

            modelBuilder.Entity<Account_Receipt>()
                .Property(e => e.LoaiChungTuGoc)
                .IsUnicode(false);

            modelBuilder.Entity<Account_ReceiptDetail>()
                .Property(e => e.MaChungTuGoc)
                .IsUnicode(false);

            modelBuilder.Entity<Account_ReceiptDetail>()
                .Property(e => e.LoaiChungTuGoc)
                .IsUnicode(false);

            modelBuilder.Entity<Account_ReceiptDetail>()
                .Property(e => e.Amount)
                .HasPrecision(18, 0);

            modelBuilder.Entity<Account_Transaction>()
                .Property(e => e.TransactionModule)
                .IsUnicode(false);

            modelBuilder.Entity<Account_Transaction>()
                .Property(e => e.TransactionCode)
                .IsUnicode(false);

            modelBuilder.Entity<Account_TransactionLiabilities>()
                .Property(e => e.TransactionModule)
                .IsUnicode(false);

            modelBuilder.Entity<Account_TransactionLiabilities>()
                .Property(e => e.TransactionCode)
                .IsUnicode(false);

            modelBuilder.Entity<Account_TransactionLiabilities>()
                .Property(e => e.TargetCode)
                .IsUnicode(false);

            modelBuilder.Entity<Account_TransactionLiabilities>()
                .Property(e => e.Debit)
                .HasPrecision(18, 0);

            modelBuilder.Entity<Account_TransactionLiabilities>()
                .Property(e => e.Credit)
                .HasPrecision(18, 0);

            modelBuilder.Entity<Account_TransactionLiabilities>()
                .Property(e => e.MaChungTuGoc)
                .IsUnicode(false);

            modelBuilder.Entity<Account_TransactionLiabilities>()
                .Property(e => e.LoaiChungTuGoc)
                .IsUnicode(false);

            modelBuilder.Entity<Account_TransactionRelationship>()
                .Property(e => e.TransactionA)
                .IsUnicode(false);

            modelBuilder.Entity<Account_TransactionRelationship>()
                .Property(e => e.TransactionB)
                .IsUnicode(false);

            modelBuilder.Entity<Crm_EmailLog>()
                .Property(e => e.HOADON_ID)
                .HasPrecision(18, 0);

            modelBuilder.Entity<Crm_Process>()
                .Property(e => e.ActivateAs)
                .IsUnicode(false);

            modelBuilder.Entity<Crm_Process>()
                .Property(e => e.DataSource)
                .IsUnicode(false);

            modelBuilder.Entity<Crm_ProcessAction>()
                .Property(e => e.ActionType)
                .IsUnicode(false);

            modelBuilder.Entity<Crm_ProcessStep>()
                .Property(e => e.StepValue)
                .IsUnicode(false);

            modelBuilder.Entity<Crm_ProcessStep>()
                .Property(e => e.EditControl)
                .IsUnicode(false);

            modelBuilder.Entity<Crm_Task>()
                .Property(e => e.ParentType)
                .IsUnicode(false);

            modelBuilder.Entity<Crm_Task>()
                .Property(e => e.Priority)
                .IsUnicode(false);

            modelBuilder.Entity<RE_Condos>()
                .Property(e => e.Code)
                .IsUnicode(false);

            modelBuilder.Entity<Sale_Commision>()
                .Property(e => e.Code)
                .IsUnicode(false);

            modelBuilder.Entity<Sale_Commision>()
                .Property(e => e.CommissionValue)
                .HasPrecision(18, 0);

            modelBuilder.Entity<Sale_Commision_Customer>()
                .Property(e => e.CommissionValue)
                .HasPrecision(18, 0);

            modelBuilder.Entity<Sale_Commision_Staff>()
                .Property(e => e.InvoiceType)
                .IsUnicode(false);

            modelBuilder.Entity<Sale_Commision_Staff>()
                .Property(e => e.AmountOfCommision)
                .HasPrecision(18, 0);

            modelBuilder.Entity<Sale_Contact>()
                .Property(e => e.WardId)
                .IsUnicode(false);

            modelBuilder.Entity<Sale_Contact>()
                .Property(e => e.DistrictId)
                .IsUnicode(false);

            modelBuilder.Entity<Sale_Customer>()
                .Property(e => e.Code)
                .IsUnicode(false);

            modelBuilder.Entity<Sale_Customer>()
                .Property(e => e.WardId)
                .IsUnicode(false);

            modelBuilder.Entity<Sale_Customer>()
                .Property(e => e.DistrictId)
                .IsUnicode(false);

            modelBuilder.Entity<Sale_Customer>()
                .Property(e => e.CompanyNameSearch)
                .IsUnicode(false);

            modelBuilder.Entity<Sale_Customer>()
                .Property(e => e.TaxCode)
                .IsUnicode(false);

            modelBuilder.Entity<Sale_Customer>()
                .Property(e => e.BankAccount)
                .IsUnicode(false);

            modelBuilder.Entity<Sale_CustomerDiscount>()
                .Property(e => e.Code)
                .IsUnicode(false);

            modelBuilder.Entity<Sale_LoyaltyPoint>()
                .Property(e => e.MinMoney)
                .HasPrecision(18, 0);

            modelBuilder.Entity<Sale_LoyaltyPoint>()
                .Property(e => e.MaxMoney)
                .HasPrecision(18, 0);

            modelBuilder.Entity<Sale_ObjectAttribute>()
                .Property(e => e.DataType)
                .IsUnicode(false);

            modelBuilder.Entity<Sale_PhysicalInventoryDetail>()
                .Property(e => e.ReferenceVoucher)
                .IsUnicode(false);

            modelBuilder.Entity<Sale_Product>()
                 .Property(e => e.PriceInbound)
                 .HasPrecision(18, 0);

            modelBuilder.Entity<Sale_Product>()
                .Property(e => e.PriceOutBound)
                .HasPrecision(18, 0);

            modelBuilder.Entity<Sale_Product>()
                .Property(e => e.Barcode)
                .IsUnicode(false);

            modelBuilder.Entity<Sale_Product>()
                .Property(e => e.Image_Name)
                .IsUnicode(false);

            modelBuilder.Entity<Sale_Product>()
                .Property(e => e.Size)
                .IsUnicode(false);

            modelBuilder.Entity<Sale_Product>()
                .Property(e => e.color)
                .IsUnicode(false);

            modelBuilder.Entity<Sale_Product>()
                .Property(e => e.NHOMSANPHAM_ID_LST)
                .IsUnicode(false);

            modelBuilder.Entity<Sale_Product>()
                .Property(e => e.LOAISANPHAM_ID_LST)
                .IsUnicode(false);

            modelBuilder.Entity<Sale_Product>()
                .Property(e => e.id_google_product_category)
                .IsUnicode(false);

            modelBuilder.Entity<Sale_ProductInbound>()
                .Property(e => e.TotalAmount)
                .HasPrecision(18, 0);

            modelBuilder.Entity<Sale_ProductInvoice>()
                .Property(e => e.TotalAmount)
                .HasPrecision(18, 0);

            modelBuilder.Entity<Sale_ProductInvoice>()
                .Property(e => e.PaidAmount)
                .HasPrecision(18, 0);

            modelBuilder.Entity<Sale_ProductInvoice>()
                .Property(e => e.RemainingAmount)
                .HasPrecision(18, 0);

            modelBuilder.Entity<Sale_ProductInvoice>()
                .Property(e => e.BarCode)
                .IsUnicode(false);

            modelBuilder.Entity<Sale_ProductInvoice>()
                .Property(e => e.FixedDiscount)
                .HasPrecision(18, 0);

            modelBuilder.Entity<Sale_ProductInvoice>()
                .Property(e => e.IrregularDiscount)
                .HasPrecision(18, 0);

            modelBuilder.Entity<Sale_ProductInvoice>()
                .Property(e => e.TaxCode)
                .IsUnicode(false);

            modelBuilder.Entity<Sale_ProductInvoice>()
                .Property(e => e.BankAccount)
                .IsUnicode(false);

            modelBuilder.Entity<Sale_ProductInvoiceDetail>()
                .Property(e => e.Price)
                .HasPrecision(18, 0);

            modelBuilder.Entity<Sale_ProductInvoiceDetail>()
                .Property(e => e.ProductType)
                .IsUnicode(false);

            modelBuilder.Entity<Sale_ProductInvoiceDetail>()
                .Property(e => e.IrregularDiscountAmount)
                .HasPrecision(18, 0);

            modelBuilder.Entity<Sale_ProductInvoiceDetail>()
                .Property(e => e.FixedDiscountAmount)
                .HasPrecision(18, 0);

            modelBuilder.Entity<Sale_ProductInvoiceDetail>()
                .Property(e => e.TaxFeeAmount)
                .HasPrecision(18, 0);

            modelBuilder.Entity<Sale_ProductOutbound>()
                .Property(e => e.TotalAmount)
                .HasPrecision(18, 0);

            modelBuilder.Entity<Sale_ProductOutboundDetail>()
                .Property(e => e.Price)
                .HasPrecision(18, 0);

            modelBuilder.Entity<Sale_Promotion>()
                .Property(e => e.Code)
                .IsUnicode(false);

            modelBuilder.Entity<Sale_PromotionDetail>()
                .Property(e => e.Type)
                .IsUnicode(false);

            modelBuilder.Entity<Sale_PurchaseOrder>()
                .Property(e => e.TotalAmount)
                .HasPrecision(18, 0);

            modelBuilder.Entity<Sale_PurchaseOrder>()
                .Property(e => e.DiscountCode)
                .IsUnicode(false);

            modelBuilder.Entity<Sale_PurchaseOrder>()
                .Property(e => e.Phone)
                .IsUnicode(false);

            modelBuilder.Entity<Sale_PurchaseOrder>()
                .Property(e => e.PaidAmount)
                .HasPrecision(18, 0);

            modelBuilder.Entity<Sale_PurchaseOrder>()
                .Property(e => e.RemainingAmount)
                .HasPrecision(18, 0);

            modelBuilder.Entity<Sale_PurchaseOrder>()
                .Property(e => e.BarCode)
                .IsUnicode(false);

            modelBuilder.Entity<Sale_PurchaseOrder>()
                .Property(e => e.Type)
                .IsUnicode(false);

            modelBuilder.Entity<Sale_RequestInbound>()
                .Property(e => e.TotalAmount)
                .HasPrecision(18, 0);

            modelBuilder.Entity<Sale_RequestInboundDetail>()
                .Property(e => e.Price)
                .HasPrecision(18, 0);

            modelBuilder.Entity<Sale_SalesReturns>()
                .Property(e => e.Code)
                .IsUnicode(false);

            modelBuilder.Entity<Sale_ServiceInvoice>()
                .Property(e => e.TotalAmount)
                .HasPrecision(18, 0);

            modelBuilder.Entity<Sale_ServiceInvoice>()
                .Property(e => e.DiscountCode)
                .IsUnicode(false);

            modelBuilder.Entity<Sale_ServiceInvoice>()
                .Property(e => e.BarCode)
                .IsUnicode(false);

            modelBuilder.Entity<Sale_ServiceInvoice>()
                .Property(e => e.RemainingAmount)
                .HasPrecision(18, 0);

            modelBuilder.Entity<Sale_ServiceInvoice>()
                .Property(e => e.PaidAmount)
                .HasPrecision(18, 0);

            modelBuilder.Entity<Sale_Supplier>()
                .Property(e => e.Code)
                .IsUnicode(false);

            modelBuilder.Entity<Sale_Supplier>()
                .Property(e => e.WardId)
                .IsUnicode(false);

            modelBuilder.Entity<Sale_Supplier>()
                .Property(e => e.DistrictId)
                .IsUnicode(false);

            modelBuilder.Entity<Sale_Supplier>()
                .Property(e => e.ProductIdOfSupplier)
                .IsUnicode(false);

            modelBuilder.Entity<Sale_Supplier>()
                .Property(e => e.TaxCode)
                .IsUnicode(false);

            modelBuilder.Entity<Sale_TotalDiscountMoneyNT>()
                .Property(e => e.PercentDecrease)
                .HasPrecision(18, 0);

            modelBuilder.Entity<Sale_TotalDiscountMoneyNT>()
                .Property(e => e.DiscountAmount)
                .HasPrecision(18, 0);

            modelBuilder.Entity<Sale_TotalDiscountMoneyNT>()
                .Property(e => e.DecreaseAmount_)
                .HasPrecision(18, 0);

            modelBuilder.Entity<Sale_TotalDiscountMoneyNT>()
                .Property(e => e.RemainingAmount)
                .HasPrecision(18, 0);

            modelBuilder.Entity<Sale_Warehouse>()
                .Property(e => e.Code)
                .IsUnicode(false);

            modelBuilder.Entity<Sale_Warehouse>()
                .Property(e => e.WardId)
                .IsUnicode(false);

            modelBuilder.Entity<Sale_Warehouse>()
                .Property(e => e.DistrictId)
                .IsUnicode(false);

            modelBuilder.Entity<Sale_WarehouseLocationItem>()
                .Property(e => e.SN)
                .IsUnicode(false);

            modelBuilder.Entity<Sale_WarehouseLocationItem>()
                .Property(e => e.Shelf)
                .IsUnicode(false);

            modelBuilder.Entity<Sale_WarehouseLocationItem>()
                .Property(e => e.Floor)
                .IsUnicode(false);

            modelBuilder.Entity<Sale_WarehouseLocationItem>()
                .Property(e => e.Position)
                .IsUnicode(false);

            modelBuilder.Entity<Staff_Branch>()
               .Property(e => e.Code)
               .IsUnicode(false);

            modelBuilder.Entity<Staff_Branch>()
                .Property(e => e.Type)
                .IsUnicode(false);

            modelBuilder.Entity<Staff_Branch>()
                .Property(e => e.MaxDebitAmount)
                .HasPrecision(18, 0);

            modelBuilder.Entity<Staff_BranchDepartment>()
                .Property(e => e.MaxDebitAmount)
                .HasPrecision(18, 0);

            modelBuilder.Entity<Staff_DotBCBHXHDetail>()
                .Property(e => e.MedicalDefaultValue)
                .HasPrecision(18, 0);

            modelBuilder.Entity<Staff_DotBCBHXHDetail>()
                .Property(e => e.SocietyDefaultValue)
                .HasPrecision(18, 0);

            modelBuilder.Entity<Staff_DotBCBHXHDetail>()
                .Property(e => e.TienLuong)
                .HasPrecision(18, 0);

            modelBuilder.Entity<Staff_FingerPrint>()
                .Property(e => e.TmpData)
                .IsUnicode(false);

            modelBuilder.Entity<Staff_FingerPrint>()
                .Property(e => e.Password)
                .IsUnicode(false);

            modelBuilder.Entity<Staff_FPMachine>()
                .Property(e => e.Ten_may_tinh)
                .IsUnicode(false);

            modelBuilder.Entity<Staff_FPMachine>()
                .Property(e => e.TeamviewerID)
                .IsUnicode(false);

            modelBuilder.Entity<Staff_FPMachine>()
                .Property(e => e.TeamviewerPassword)
                .IsUnicode(false);

            modelBuilder.Entity<Staff_FPMachine>()
                .Property(e => e.GetDataSchedule)
                .IsUnicode(false);

            modelBuilder.Entity<Staff_GiamTruThueTNCN>()
                .Property(e => e.Value)
                .HasPrecision(18, 0);

            modelBuilder.Entity<Staff_HistoryCommissionStaff>()
                .Property(e => e.CommissionPercent)
                .HasPrecision(18, 0);

            modelBuilder.Entity<Staff_HistoryCommissionStaff>()
                .Property(e => e.MinimumRevenue)
                .HasPrecision(18, 0);

            modelBuilder.Entity<Staff_HistoryCommissionStaff>()
                .Property(e => e.RevenueDS)
                .HasPrecision(18, 0);

            modelBuilder.Entity<Staff_HistoryCommissionStaff>()
                .Property(e => e.AmountCommission)
                .HasPrecision(18, 0);

            modelBuilder.Entity<Staff_Position>()
                .Property(e => e.CommissionPercent)
                .HasPrecision(18, 0);

            modelBuilder.Entity<Staff_Position>()
                .Property(e => e.MinimumRevenue)
                .HasPrecision(18, 0);

            modelBuilder.Entity<Staff_SalarySettingDetail>()
                .Property(e => e.FormulaType)
                .IsUnicode(false);

            modelBuilder.Entity<Staff_SalarySettingDetail>()
                .Property(e => e.DataType)
                .IsUnicode(false);

            modelBuilder.Entity<Staff_SalarySettingDetail_Staff>()
                .Property(e => e.DefaultValue)
                .HasPrecision(18, 0);

            modelBuilder.Entity<Staff_SalaryTable>()
                .Property(e => e.TotalSalary)
                .HasPrecision(18, 0);

            modelBuilder.Entity<Staff_SalaryTableDetail_Report>()
                .Property(e => e.DataType)
                .IsUnicode(false);

            modelBuilder.Entity<Staff_SocialInsurance>()
                .Property(e => e.MedicalDefaultValue)
                .HasPrecision(18, 0);

            modelBuilder.Entity<Staff_SocialInsurance>()
                .Property(e => e.SocietyDefaultValue)
                .HasPrecision(18, 0);

            modelBuilder.Entity<Staff_SocialInsurance>()
                .Property(e => e.TienLuong)
                .HasPrecision(18, 0);

            modelBuilder.Entity<Staff_Staff>()
                .Property(e => e.CommissionPercent)
                .HasPrecision(18, 0);

            modelBuilder.Entity<Staff_Staff>()
                .Property(e => e.MinimumRevenue)
                .HasPrecision(18, 0);

            modelBuilder.Entity<Staff_TaxRate>()
                .Property(e => e.FromValue)
                .HasPrecision(18, 0);

            modelBuilder.Entity<Staff_TaxRate>()
                .Property(e => e.ToValue)
                .HasPrecision(18, 0);

            modelBuilder.Entity<Staff_ThuNhapChiuThue>()
                .Property(e => e.Value)
                .HasPrecision(18, 0);

            modelBuilder.Entity<System_Language>()
                .Property(e => e.Id)
                .IsUnicode(false);

            modelBuilder.Entity<System_Module>()
                .Property(e => e.TableName)
                .IsUnicode(false);

            modelBuilder.Entity<System_Module>()
                .Property(e => e.AreaName)
                .IsUnicode(false);

            modelBuilder.Entity<System_ModuleRelationship>()
                .Property(e => e.First_ModuleName)
                .IsUnicode(false);

            modelBuilder.Entity<System_ModuleRelationship>()
                .Property(e => e.First_MetadataFieldName)
                .IsUnicode(false);

            modelBuilder.Entity<System_ModuleRelationship>()
                .Property(e => e.Second_ModuleName)
                .IsUnicode(false);

            modelBuilder.Entity<System_ModuleRelationship>()
                .Property(e => e.Second_MetadataFieldName)
                .IsUnicode(false);

            modelBuilder.Entity<System_ModuleRelationship>()
                .Property(e => e.Second_ModuleName_Alias)
                .IsUnicode(false);

            modelBuilder.Entity<System_News>()
                .Property(e => e.Url)
                .IsUnicode(false);

            modelBuilder.Entity<System_PageMenu>()
                .Property(e => e.LanguageId)
                .IsUnicode(false);

            modelBuilder.Entity<System_PageMenu>()
                .Property(e => e.CssClassIcon)
                .IsUnicode(false);

            modelBuilder.Entity<System_PageMenu>()
                .Property(e => e.DashboardView)
                .IsUnicode(false);

            modelBuilder.Entity<System_User>()
                .Property(e => e.UserCode)
                .IsUnicode(false);

            modelBuilder.Entity<System_User>()
                .Property(e => e.Email)
                .IsUnicode(false);

            modelBuilder.Entity<webpages_Roles>()
                .HasMany(e => e.System_User)
                .WithMany(e => e.webpages_Roles)
                .Map(m => m.ToTable("webpages_UsersInRoles").MapLeftKey("RoleId").MapRightKey("UserId"));

            modelBuilder.Entity<Staff_HealthInsurance>()
                .Property(e => e.DefaultValue)
                .HasPrecision(18, 0);
        }
    }
}
