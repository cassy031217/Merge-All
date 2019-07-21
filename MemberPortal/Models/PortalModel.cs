namespace MemberPortal.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class PortalModel : DbContext
    {
        public PortalModel()
            : base("name=PortalModel")
        {
        }

        public virtual DbSet<LoanApplication> LoanApplications { get; set; }
        public virtual DbSet<LoanProductsMaster> LoanProductsMasters { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<LoanApplication>()
                .Property(e => e.MemberCode)
                .IsUnicode(false);

            modelBuilder.Entity<LoanApplication>()
                .Property(e => e.Product)
                .IsUnicode(false);

            modelBuilder.Entity<LoanApplication>()
                .Property(e => e.ApplicationFormPath)
                .IsUnicode(false);

            modelBuilder.Entity<LoanApplication>()
                .Property(e => e.File1Path)
                .IsUnicode(false);

            modelBuilder.Entity<LoanApplication>()
                .Property(e => e.File2Path)
                .IsUnicode(false);

            modelBuilder.Entity<LoanApplication>()
                .Property(e => e.File3Path)
                .IsUnicode(false);

            modelBuilder.Entity<LoanApplication>()
                .Property(e => e.File4Path)
                .IsUnicode(false);

            modelBuilder.Entity<LoanApplication>()
                .Property(e => e.File5Path)
                .IsUnicode(false);

            modelBuilder.Entity<LoanApplication>()
                .Property(e => e.VerificationStatus)
                .IsUnicode(false);

            modelBuilder.Entity<LoanApplication>()
                .Property(e => e.VerificationRemarks)
                .IsUnicode(false);

            modelBuilder.Entity<LoanApplication>()
                .Property(e => e.EvaluationStatus)
                .IsUnicode(false);

            modelBuilder.Entity<LoanApplication>()
                .Property(e => e.ClientEvalRemarks)
                .IsUnicode(false);

            modelBuilder.Entity<LoanApplication>()
                .Property(e => e.EvaluatorRemarks)
                .IsUnicode(false);

            modelBuilder.Entity<LoanApplication>()
                .Property(e => e.ApprovalStatus)
                .IsUnicode(false);

            modelBuilder.Entity<LoanApplication>()
                .Property(e => e.ApproverRemarks)
                .IsUnicode(false);

            modelBuilder.Entity<LoanApplication>()
                .Property(e => e.AddedReqPath1)
                .IsUnicode(false);

            modelBuilder.Entity<LoanApplication>()
                .Property(e => e.AddedReqPath2)
                .IsUnicode(false);

            modelBuilder.Entity<LoanApplication>()
                .Property(e => e.AddedReqPath3)
                .IsUnicode(false);

            modelBuilder.Entity<LoanApplication>()
                .Property(e => e.ReleaseRemarks)
                .IsUnicode(false);

            modelBuilder.Entity<LoanProductsMaster>()
                .Property(e => e.ProductCode)
                .IsUnicode(false);

            modelBuilder.Entity<LoanProductsMaster>()
                .Property(e => e.ProductName)
                .IsUnicode(false);
        }
    }
}
