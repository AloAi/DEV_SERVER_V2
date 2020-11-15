using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Aloai.Models
{
    public class AloaiDataContext : DbContext
    {
        public AloaiDataContext(DbContextOptions<AloaiDataContext> options)
            : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.EnableSensitiveDataLogging(true);
        }

        public DbSet<D_JOB> D_JOBS { get; set; }

        public DbSet<Template> Templates { get; set; }

        public DbSet<M_USER> M_USERS { get; set; }

        public DbSet<M_PARTNER_INFO> M_PARTNER_INFOS { get; set; }

        public DbSet<M_HIRER_INFO> M_HIRER_INFOS { get; set; }

        public DbSet<T_PARTNER_CATALOG_UNIT> T_PARTNER_CATALOG_UNITS { get; set; }

        public DbSet<M_DEFINE> M_DEFINES { get; set; }

        public DbSet<M_CATALOG> M_CATALOGS { get; set; }

        public DbSet<M_UNIT> M_UNITS { get; set; }

        public DbSet<M_QUESTION> M_QUESTIONS { get; set; }

        public DbSet<M_NAME> M_NAMES { get; set; }

        public DbSet<M_MESSAGE> M_MESSAGES { get; set; }

        public DbSet<M_SYSTEM_MESSAGE> M_SYSTEM_MESSAGES { get; set; }

        public DbSet<M_LOCATION> M_LOCATIONS { get; set; }

        public DbSet<D_REVIEW> D_REVIEWS{ get; set; }

        public DbSet<D_CONTACT> D_CONTACTS { get; set; }

        public DbSet<D_NOTIFY> D_NOTIFYS { get; set; }

        public DbSet<D_HISTORY> D_HISTORYS { get; set; }

        public DbSet<D_FAVOURITE> D_FAVOURITES { get; set; }

        public DbSet<M_IMAGE_DETAIL> M_IMAGE_DETAILS { get; set; }

        public DbSet<V_JOB> V_JOBS { get; set; }

        public DbSet<V_PARTNER> V_PARTNERS { get; set; }

        //public DbSet<V_EXCHANGE> V_EXCHANGES { get; set; }

        public DbSet<V_FAVOURITE> V_FAVOURITES { get; set; }

        public DbSet<V_CONTACT_HISTORY> V_CONTACT_HISTORYS { get; set; }
        public DbSet<V_CONTACT_INFO> V_CONTACT_INFOS { get; set; }

        public DbSet<V_SUGGEST_JOB> V_SUGGEST_JOBS { get; set; }

        /// <summary>
        /// Thiết lập Fluent API kế thừa từ DBContext
        /// </summary>
        /// <param name="builder">Dùng để thiết lập các Entities</param>
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            #region Table
            builder.Entity<Template>(entity =>
            {
                entity.ToTable("M_TEMPLATE");
                entity.HasKey(x => x.TemplateCd).HasName("M_TEMPLATE_PK");

                PropertyBuilder<decimal> id = entity.Property(e => e.TemplateCd);
                id.HasColumnName("TEMPLATE_CD");
                id.HasColumnType("DECIMAL");
                id.Metadata.SetBeforeSaveBehavior(PropertySaveBehavior.Ignore);

                entity.Property(e => e.TemplateCd).HasColumnName("TEMPLATE_CD").HasColumnType("DECIMAL").HasMaxLength(9);
                entity.Property(e => e.TemplateTitle).HasColumnName("TEMPLATE_TITLE").HasColumnType("NVARCHAR(100)").HasMaxLength(100);
                entity.Property(e => e.TemplateTitleEn).HasColumnName("TEMPLATE_TITLE_EN").HasColumnType("NVARCHAR(100)").HasMaxLength(100);
                entity.Property(e => e.DispOrder).HasColumnName("DISP_ORDER").HasColumnType("DECIMAL").HasMaxLength(3);
                entity.Property(e => e.DeleteFlg).HasColumnName("DELETE_FLG").HasColumnType("DECIMAL").HasMaxLength(1);
                entity.Property(e => e.RegDatetime).HasColumnName("REG_DATETIME").HasColumnType("DATETIME");
                entity.Property(e => e.UpdDatetime).HasColumnName("UPD_DATETIME").HasColumnType("DATETIME");
            });

            builder.Entity<D_JOB>(entity =>
            {
                entity.ToTable("D_JOB");
                entity.HasKey(x => x.JOB_ID).HasName("D_JOB_PK");

                PropertyBuilder<decimal> id = entity.Property(e => e.JOB_ID);
                id.HasColumnName("JOB_ID");
                id.HasColumnType("DECIMAL");
                id.UseIdentityColumn();
                id.Metadata.SetBeforeSaveBehavior(PropertySaveBehavior.Ignore);

                entity.Property(e => e.JOB_ID).HasColumnName("JOB_ID").HasColumnType("DECIMAL").HasMaxLength(9);
                entity.Property(e => e.USER_ID).HasColumnName("USER_ID").HasColumnType("DECIMAL").HasMaxLength(9);
                entity.Property(e => e.SUGGEST_ID).HasColumnName("SUGGEST_ID").HasColumnType("DECIMAL").HasMaxLength(9);
                entity.Property(e => e.CANCEL_FLG).HasColumnName("CANCEL_FLG").HasColumnType("DECIMAL").HasMaxLength(1);
                entity.Property(e => e.RENEW_NUM).HasColumnName("RENEW_NUM").HasColumnType("DECIMAL").HasMaxLength(2);
                entity.Property(e => e.LATITUDE).HasColumnName("LONGITUDE").HasColumnType("NVARCHAR(20)").HasMaxLength(20);
                entity.Property(e => e.LONGITUDE).HasColumnName("LATITUDE").HasColumnType("NVARCHAR(20)").HasMaxLength(20);
                entity.Property(e => e.REG_DATETIME).HasColumnName("REG_DATETIME").HasColumnType("DATETIME");
                entity.Property(e => e.UPD_DATETIME).HasColumnName("UPD_DATETIME").HasColumnType("DATETIME");
            });

            builder.Entity<M_IMAGE_DETAIL>(entity =>
            {
                entity.ToTable("M_IMAGE_DETAIL");
                entity.HasKey(x => x.ID).HasName("M_IMAGE_DETAIL_PK");

                PropertyBuilder<decimal> id = entity.Property(e => e.ID);
                id.HasColumnName("ID");
                id.HasColumnType("DECIMAL");
                id.UseIdentityColumn();
                id.Metadata.SetBeforeSaveBehavior(PropertySaveBehavior.Ignore);

                entity.Property(e => e.ID).HasColumnName("ID").HasColumnType("DECIMAL").HasMaxLength(9);
                entity.Property(e => e.OBJECT_TYPE).HasColumnName("OBJECT_TYPE").HasColumnType("DECIMAL").HasMaxLength(1);
                entity.Property(e => e.ROW_NO).HasColumnName("ROW_NO").HasColumnType("DECIMAL").HasMaxLength(2);
                entity.Property(e => e.IMAGE_NAME).HasColumnName("IMAGE_NAME").HasColumnType("NVARCHAR(300)").HasMaxLength(300);
                entity.Property(e => e.IMAGE_PATH).HasColumnName("IMAGE_PATH").HasColumnType("NVARCHAR(300)").HasMaxLength(300);
            });

            builder.Entity<M_USER>(entity =>
            {
                entity.ToTable("M_USER");
                entity.HasKey(x => x.USER_ID).HasName("M_USER_PK");

                PropertyBuilder<decimal> id = entity.Property(e => e.USER_ID);
                id.HasColumnName("USER_ID");
                id.HasColumnType("DECIMAL");
                id.UseIdentityColumn();
                id.Metadata.SetBeforeSaveBehavior(PropertySaveBehavior.Ignore);

                entity.Property(e => e.USER_ID).HasColumnName("USER_ID").HasColumnType("DECIMAL").HasMaxLength(9);
                entity.Property(e => e.PHONE_NUMBER).HasColumnName("PHONE_NUMBER").HasColumnType("NVARCHAR(20)").HasMaxLength(20);
                entity.Property(e => e.NAME).HasColumnName("NAME").HasColumnType("NVARCHAR(50)").HasMaxLength(50);
                entity.Property(e => e.AVATAR).HasColumnName("AVATAR").HasColumnType("NVARCHAR(MAX)");
                entity.Property(e => e.MODE_DEFAULT).HasColumnName("MODE_DEFAULT").HasColumnType("DECIMAL").HasMaxLength(1);
                entity.Property(e => e.MODE_USER).HasColumnName("MODE_USER").HasColumnType("DECIMAL").HasMaxLength(1);
                entity.Property(e => e.SIGNIN_LAST).HasColumnName("SIGNIN_LAST").HasColumnType("DATETIME");
                entity.Property(e => e.LANGUAGE_TYPE).HasColumnName("LANGUAGE_TYPE").HasColumnType("NVARCHAR(5)").HasMaxLength(5);
                entity.Property(e => e.BLOCK_FLG).HasColumnName("BLOCK_FLG").HasColumnType("DECIMAL").HasMaxLength(1);
                entity.Property(e => e.DELETE_FLG).HasColumnName("DELETE_FLG").HasColumnType("DECIMAL").HasMaxLength(1);
                entity.Property(e => e.REG_DATETIME).HasColumnName("REG_DATETIME").HasColumnType("DATETIME");
                entity.Property(e => e.UPD_DATETIME).HasColumnName("UPD_DATETIME").HasColumnType("DATETIME");
            });

            builder.Entity<M_PARTNER_INFO>(entity =>
            {
                entity.ToTable("M_PARTNER_INFO");
                entity.HasKey(x => x.USER_ID).HasName("M_PARTNER_INFO_PK");

                PropertyBuilder<decimal> id = entity.Property(e => e.USER_ID);
                id.HasColumnName("USER_ID");
                id.HasColumnType("DECIMAL");
                //id.Metadata.SetBeforeSaveBehavior(PropertySaveBehavior.Ignore);

                entity.Property(e => e.USER_ID).HasColumnName("USER_ID").HasColumnType("DECIMAL").HasMaxLength(9);
                entity.Property(e => e.INTRODUCE).HasColumnName("INTRODUCE").HasColumnType("NVARCHAR(1000)").HasMaxLength(1000);
                entity.Property(e => e.FIX_LOCATION_FLG).HasColumnName("FIX_LOCATION_FLG").HasColumnType("DECIMAL").HasMaxLength(1);
                entity.Property(e => e.LONGITUDE).HasColumnName("LONGITUDE").HasColumnType("NVARCHAR(20)").HasMaxLength(20);
                entity.Property(e => e.LATITUDE).HasColumnName("LATITUDE").HasColumnType("NVARCHAR(20)").HasMaxLength(20);
                entity.Property(e => e.ADDRESS).HasColumnName("ADDRESS").HasColumnType("NVARCHAR(200)").HasMaxLength(200);
                entity.Property(e => e.VERIFY_FLG).HasColumnName("VERIFY_FLG").HasColumnType("DECIMAL").HasMaxLength(1);
                entity.Property(e => e.VERIFY_DATE).HasColumnName("VERIFY_DATE").HasColumnType("DATETIME");
                entity.Property(e => e.VERIFY_DATE_FROM).HasColumnName("VERIFY_DATE_FROM").HasColumnType("DATETIME");
                entity.Property(e => e.VERIFY_DATE_TO).HasColumnName("VERIFY_DATE_TO").HasColumnType("DATETIME");
                entity.Property(e => e.SCORE).HasColumnName("SCORE").HasColumnType("DECIMAL").HasMaxLength(3);
                entity.Property(e => e.LIKE_NUM).HasColumnName("LIKE_NUM").HasColumnType("DECIMAL").HasMaxLength(9);
                entity.Property(e => e.STATUS).HasColumnName("STATUS").HasColumnType("DECIMAL").HasMaxLength(1);
                entity.Property(e => e.REG_DATETIME).HasColumnName("REG_DATETIME").HasColumnType("DATETIME");
                entity.Property(e => e.UPD_DATETIME).HasColumnName("UPD_DATETIME").HasColumnType("DATETIME");
            });

            builder.Entity<M_HIRER_INFO>(entity =>
            {
                entity.ToTable("M_HIRER_INFO");
                entity.HasKey(x => x.USER_ID).HasName("M_HIRER_INFO_PK");

                PropertyBuilder<decimal> id = entity.Property(e => e.USER_ID);
                id.HasColumnName("USER_ID");
                id.HasColumnType("DECIMAL");
                //id.Metadata.SetBeforeSaveBehavior(PropertySaveBehavior.Ignore);

                entity.Property(e => e.USER_ID).HasColumnName("USER_ID").HasColumnType("DECIMAL").HasMaxLength(9);
                entity.Property(e => e.VERIFY_FLG).HasColumnName("VERIFY_FLG").HasColumnType("DECIMAL").HasMaxLength(1);
                entity.Property(e => e.VERIFY_DATE).HasColumnName("VERIFY_DATE").HasColumnType("DATETIME");
                entity.Property(e => e.VERIFY_DATE_FROM).HasColumnName("VERIFY_DATE_FROM").HasColumnType("DATETIME");
                entity.Property(e => e.VERIFY_DATE_TO).HasColumnName("VERIFY_DATE_TO").HasColumnType("DATETIME");
                entity.Property(e => e.SCORE).HasColumnName("SCORE").HasColumnType("DECIMAL").HasMaxLength(3);
                entity.Property(e => e.LIKE_NUM).HasColumnName("LIKE_NUM").HasColumnType("DECIMAL").HasMaxLength(9);
                entity.Property(e => e.STATUS).HasColumnName("STATUS").HasColumnType("DECIMAL").HasMaxLength(1);
                entity.Property(e => e.REG_DATETIME).HasColumnName("REG_DATETIME").HasColumnType("DATETIME");
                entity.Property(e => e.UPD_DATETIME).HasColumnName("UPD_DATETIME").HasColumnType("DATETIME");
            });

            builder.Entity<T_PARTNER_CATALOG_UNIT>(entity =>
            {
                entity.ToTable("T_PARTNER_CATALOG_UNIT");
                entity.HasKey(x => x.USER_ID).HasName("T_PARTNER_CATALOG_UNIT_PK");
                entity.HasKey(x => x.CATALOG_CD).HasName("T_PARTNER_CATALOG_UNIT_PK");
                entity.HasKey(x => x.UNIT_CD).HasName("T_PARTNER_CATALOG_UNIT_PK");

                PropertyBuilder<decimal> id = entity.Property(e => e.USER_ID);
                id.HasColumnName("USER_ID");
                id.HasColumnType("DECIMAL");
                //id.Metadata.SetBeforeSaveBehavior(PropertySaveBehavior.Ignore);

                entity.Property(e => e.USER_ID).HasColumnName("USER_ID").HasColumnType("DECIMAL").HasMaxLength(9);
                entity.Property(e => e.CATALOG_CD).HasColumnName("CATALOG_CD").HasColumnType("DECIMAL").HasMaxLength(5);
                entity.Property(e => e.COST).HasColumnName("COST").HasColumnType("DECIMAL").HasMaxLength(10);
                entity.Property(e => e.UNIT_CD).HasColumnName("UNIT_CD").HasColumnType("DECIMAL").HasMaxLength(5);
                entity.Property(e => e.REG_DATETIME).HasColumnName("REG_DATETIME").HasColumnType("DATETIME");
                entity.Property(e => e.UPD_DATETIME).HasColumnName("UPD_DATETIME").HasColumnType("DATETIME");
            });

            builder.Entity<M_DEFINE>(entity =>
            {
                entity.ToTable("M_DEFINE");
                entity.HasKey(x => x.CONTROL_NAME).HasName("M_DEFINE_PK");

                PropertyBuilder<string> id = entity.Property(e => e.CONTROL_NAME);
                id.HasColumnName("CONTROL_NAME");
                id.HasColumnType("DECIMAL");
                id.Metadata.SetBeforeSaveBehavior(PropertySaveBehavior.Ignore);

                entity.Property(e => e.CONTROL_NAME).HasColumnName("CONTROL_NAME").HasColumnType("NVARCHAR(50)").HasMaxLength(50);
                entity.Property(e => e.DATA_TYPE).HasColumnName("DATA_TYPE").HasColumnType("DECIMAL").HasMaxLength(1);
                entity.Property(e => e.VALUE).HasColumnName("VALUE").HasColumnType("NVARCHAR(50)").HasMaxLength(50);
                entity.Property(e => e.MEMO).HasColumnName("MEMO").HasColumnType("NVARCHAR(200)").HasMaxLength(200);
                entity.Property(e => e.REG_DATETIME).HasColumnName("REG_DATETIME").HasColumnType("DATETIME");
                entity.Property(e => e.REG_USER_ID).HasColumnName("REG_USER_ID").HasColumnType("DECIMAL").HasMaxLength(9);
                entity.Property(e => e.UPD_DATETIME).HasColumnName("UPD_DATETIME").HasColumnType("DATETIME");
                entity.Property(e => e.UPD_USER_ID).HasColumnName("UPD_USER_ID").HasColumnType("DECIMAL").HasMaxLength(9);
            });

            builder.Entity<M_CATALOG>(entity =>
            {
                entity.ToTable("M_CATALOG");
                entity.HasKey(x => x.CATALOG_CD).HasName("M_CATALOG_PK");

                PropertyBuilder<decimal> id = entity.Property(e => e.CATALOG_CD);
                id.HasColumnName("CATALOG_CD");
                id.HasColumnType("DECIMAL");
                id.Metadata.SetBeforeSaveBehavior(PropertySaveBehavior.Ignore);

                entity.Property(e => e.CATALOG_CD).HasColumnName("CATALOG_CD").HasColumnType("DECIMAL").HasMaxLength(5);
                entity.Property(e => e.CATALOG_NAME).HasColumnName("CATALOG_NAME").HasColumnType("NVARCHAR(50)").HasMaxLength(50);
                entity.Property(e => e.CATALOG_NAME_EN).HasColumnName("CATALOG_NAME_EN").HasColumnType("NVARCHAR(50)").HasMaxLength(50);
                entity.Property(e => e.DISP_ORDER).HasColumnName("DISP_ORDER").HasColumnType("DECIMAL").HasMaxLength(2);
                entity.Property(e => e.SHOW_FLG).HasColumnName("SHOW_FLG").HasColumnType("DECIMAL").HasMaxLength(1);
                entity.Property(e => e.DELETE_FLG).HasColumnName("DELETE_FLG").HasColumnType("DECIMAL").HasMaxLength(1);
                entity.Property(e => e.REG_DATETIME).HasColumnName("REG_DATETIME").HasColumnType("DATETIME");
                entity.Property(e => e.REG_USER_ID).HasColumnName("REG_USER_ID").HasColumnType("DECIMAL").HasMaxLength(9);
                entity.Property(e => e.UPD_DATETIME).HasColumnName("UPD_DATETIME").HasColumnType("DATETIME");
                entity.Property(e => e.UPD_USER_ID).HasColumnName("UPD_USER_ID").HasColumnType("DECIMAL").HasMaxLength(9);
            });

            builder.Entity<M_UNIT>(entity =>
            {
                entity.ToTable("M_UNIT");
                entity.HasKey(x => x.UNIT_CD).HasName("M_UNIT_PK");

                PropertyBuilder<decimal> id = entity.Property(e => e.UNIT_CD);
                id.HasColumnName("UNIT_CD");
                id.HasColumnType("DECIMAL");
                id.Metadata.SetBeforeSaveBehavior(PropertySaveBehavior.Ignore);

                entity.Property(e => e.UNIT_CD).HasColumnName("UNIT_CD").HasColumnType("DECIMAL").HasMaxLength(5);
                entity.Property(e => e.UNIT_NAME).HasColumnName("UNIT_NAME").HasColumnType("NVARCHAR(50)").HasMaxLength(50);
                entity.Property(e => e.UNIT_NAME_EN).HasColumnName("UNIT_NAME_EN").HasColumnType("NVARCHAR(50)").HasMaxLength(50);
                entity.Property(e => e.DISP_ORDER).HasColumnName("DISP_ORDER").HasColumnType("DECIMAL").HasMaxLength(2);
                entity.Property(e => e.SHOW_FLG).HasColumnName("SHOW_FLG").HasColumnType("DECIMAL").HasMaxLength(1);
                entity.Property(e => e.DELETE_FLG).HasColumnName("DELETE_FLG").HasColumnType("DECIMAL").HasMaxLength(1);
                entity.Property(e => e.REG_DATETIME).HasColumnName("REG_DATETIME").HasColumnType("DATETIME");
                entity.Property(e => e.REG_USER_ID).HasColumnName("REG_USER_ID").HasColumnType("DECIMAL").HasMaxLength(9);
                entity.Property(e => e.UPD_DATETIME).HasColumnName("UPD_DATETIME").HasColumnType("DATETIME");
                entity.Property(e => e.UPD_USER_ID).HasColumnName("UPD_USER_ID").HasColumnType("DECIMAL").HasMaxLength(9);
            });

            builder.Entity<M_QUESTION>(entity =>
            {
                entity.ToTable("M_QUESTION");
                entity.HasKey(x => x.FAQ_ID).HasName("M_QUESTION_PK");

                PropertyBuilder<decimal> id = entity.Property(e => e.FAQ_ID);
                id.HasColumnName("FAQ_ID");
                id.HasColumnType("DECIMAL");
                id.UseIdentityColumn();
                id.Metadata.SetBeforeSaveBehavior(PropertySaveBehavior.Ignore);

                entity.Property(e => e.FAQ_ID).HasColumnName("FAQ_ID").HasColumnType("DECIMAL").HasMaxLength(9);
                entity.Property(e => e.USER_ID).HasColumnName("USER_ID").HasColumnType("DECIMAL").HasMaxLength(9);
                entity.Property(e => e.QUESTION_DATE).HasColumnName("QUESTION_DATE").HasColumnType("DATETIME");
                entity.Property(e => e.SUBJECT).HasColumnName("SUBJECT").HasColumnType("NVARCHAR(100)").HasMaxLength(100);
                entity.Property(e => e.CONTENT).HasColumnName("CONTENT").HasColumnType("NVARCHAR(1000)").HasMaxLength(1000);
                entity.Property(e => e.REG_DATETIME).HasColumnName("REG_DATETIME").HasColumnType("DATETIME");
                entity.Property(e => e.UPD_DATETIME).HasColumnName("UPD_DATETIME").HasColumnType("DATETIME");
            });

            builder.Entity<M_NAME>(entity =>
            {
                entity.ToTable("M_NAME");
                entity.HasKey(x => x.CD).HasName("M_NAME_PK");

                entity.Property(e => e.TYPE_NAME).HasColumnName("TYPE_NAME").HasColumnType("NVARCHAR(50)").HasMaxLength(50);
                entity.Property(e => e.CD).HasColumnName("CD").HasColumnType("DECIMAL").HasMaxLength(5);
                entity.Property(e => e.NAME).HasColumnName("NAME").HasColumnType("NVARCHAR(50)").HasMaxLength(50);
                entity.Property(e => e.NAME_EN).HasColumnName("NAME_EN").HasColumnType("NVARCHAR(50)").HasMaxLength(50);
                entity.Property(e => e.DISP_ORDER).HasColumnName("DISP_ORDER").HasColumnType("DECIMAL").HasMaxLength(2);
                entity.Property(e => e.DELETE_FLG).HasColumnName("DELETE_FLG").HasColumnType("DECIMAL").HasMaxLength(1);
                entity.Property(e => e.REG_DATETIME).HasColumnName("REG_DATETIME").HasColumnType("DATETIME");
                entity.Property(e => e.REG_USER_ID).HasColumnName("REG_USER_ID").HasColumnType("DECIMAL").HasMaxLength(9);
                entity.Property(e => e.UPD_DATETIME).HasColumnName("UPD_DATETIME").HasColumnType("DATETIME");
                entity.Property(e => e.UPD_USER_ID).HasColumnName("UPD_USER_ID").HasColumnType("DECIMAL").HasMaxLength(9);
            });

            builder.Entity<M_MESSAGE>(entity =>
            {
                entity.ToTable("M_MESSAGE");
                entity.HasKey(x => x.MESSAGE_ID).HasName("M_MESSAGE_PK");

                PropertyBuilder<decimal> id = entity.Property(e => e.MESSAGE_ID);
                id.HasColumnName("MESSAGE_ID");
                id.HasColumnType("DECIMAL");
                id.UseIdentityColumn();
                id.Metadata.SetBeforeSaveBehavior(PropertySaveBehavior.Ignore);

                entity.Property(e => e.MESSAGE_ID).HasColumnName("MESSAGE_ID").HasColumnType("DECIMAL").HasMaxLength(9);
                entity.Property(e => e.MESSAGE_CD).HasColumnName("MESSAGE_CD").HasColumnType("DECIMAL").HasMaxLength(3);
                entity.Property(e => e.LANGUAGE_TYPE).HasColumnName("LANGUAGE_TYPE").HasColumnType("NVARCHAR(5)").HasMaxLength(5);
                entity.Property(e => e.MESSAGE_CONTENT).HasColumnName("MESSAGE_CONTENT").HasColumnType("NVARCHAR(300)").HasMaxLength(300);
                entity.Property(e => e.REG_DATETIME).HasColumnName("REG_DATETIME").HasColumnType("DATETIME");
                entity.Property(e => e.REG_USER_ID).HasColumnName("REG_USER_ID").HasColumnType("DECIMAL").HasMaxLength(9);
            });

            builder.Entity<M_SYSTEM_MESSAGE>(entity =>
            {
                entity.ToTable("M_SYSTEM_MESSAGE");
                entity.HasKey(x => x.MESSAGE_ID).HasName("M_SYSTEM_MESSAGE_PK");

                PropertyBuilder<decimal> id = entity.Property(e => e.MESSAGE_ID);
                id.HasColumnName("MESSAGE_ID");
                id.HasColumnType("DECIMAL");
                id.UseIdentityColumn();
                id.Metadata.SetBeforeSaveBehavior(PropertySaveBehavior.Ignore);

                entity.Property(e => e.MESSAGE_ID).HasColumnName("MESSAGE_ID").HasColumnType("DECIMAL").HasMaxLength(9);
                entity.Property(e => e.MESSAGE_CD).HasColumnName("MESSAGE_CD").HasColumnType("DECIMAL").HasMaxLength(9);
                entity.Property(e => e.LANGUAGE_TYPE).HasColumnName("LANGUAGE_TYPE").HasColumnType("NVARCHAR(5)").HasMaxLength(5);
                entity.Property(e => e.MESSAGE).HasColumnName("MESSAGE").HasColumnType("NVARCHAR(300)").HasMaxLength(300);
                entity.Property(e => e.MESSAGE_CONTENT).HasColumnName("MESSAGE_CONTENT").HasColumnType("NVARCHAR(300)").HasMaxLength(300);
                entity.Property(e => e.REG_DATETIME).HasColumnName("REG_DATETIME").HasColumnType("DATETIME");
                entity.Property(e => e.REG_USER_ID).HasColumnName("REG_USER_ID").HasColumnType("DECIMAL").HasMaxLength(9);
            });

            builder.Entity<M_LOCATION>(entity =>
            {
                entity.ToTable("M_LOCATION");
                entity.HasKey(x => x.CD).HasName("M_LOCATION_PK");

                PropertyBuilder<decimal> id = entity.Property(e => e.CD);
                id.HasColumnName("CD");
                id.HasColumnType("DECIMAL");
                id.Metadata.SetBeforeSaveBehavior(PropertySaveBehavior.Ignore);

                entity.Property(e => e.CD).HasColumnName("CD").HasColumnType("DECIMAL").HasMaxLength(9);
                entity.Property(e => e.NAME).HasColumnName("NAME").HasColumnType("NVARCHAR(50)").HasMaxLength(50);
                entity.Property(e => e.LONGITUDE_EAST).HasColumnName("LONGITUDE_EAST").HasColumnType("NVARCHAR(20)").HasMaxLength(20);
                entity.Property(e => e.LONGITUDE_WEST).HasColumnName("LONGITUDE_WEST").HasColumnType("NVARCHAR(20)").HasMaxLength(20);
                entity.Property(e => e.LATITUDE_SOUTH).HasColumnName("LATITUDE_SOUTH").HasColumnType("NVARCHAR(20)").HasMaxLength(20);
                entity.Property(e => e.LATITUDE_NORTH).HasColumnName("LATITUDE_NORTH").HasColumnType("NVARCHAR(20)").HasMaxLength(20);
                entity.Property(e => e.REG_USER_ID).HasColumnName("REG_USER_ID").HasColumnType("DECIMAL").HasMaxLength(9);
                entity.Property(e => e.UPD_DATETIME).HasColumnName("UPD_DATETIME").HasColumnType("DATETIME");
                entity.Property(e => e.UPD_USER_ID).HasColumnName("UPD_USER_ID").HasColumnType("DECIMAL").HasMaxLength(9);
            });

            builder.Entity<D_REVIEW>(entity =>
            {
                entity.ToTable("D_REVIEW");
                entity.HasKey(x => x.REVIEW_ID).HasName("D_REVIEW_PK");

                PropertyBuilder<decimal> id = entity.Property(e => e.REVIEW_ID);
                id.HasColumnName("REVIEW_ID");
                id.HasColumnType("DECIMAL");
                id.UseIdentityColumn();
                id.Metadata.SetBeforeSaveBehavior(PropertySaveBehavior.Ignore);

                entity.Property(e => e.REVIEW_ID).HasColumnName("REVIEW_ID").HasColumnType("DECIMAL").HasMaxLength(9);
                entity.Property(e => e.CONTACT_ID).HasColumnName("CONTACT_ID").HasColumnType("DECIMAL").HasMaxLength(9);
                entity.Property(e => e.REVIEW_USER_ID).HasColumnName("REVIEW_USER_ID").HasColumnType("DECIMAL").HasMaxLength(9);
                entity.Property(e => e.REVIEW_MODE_USER).HasColumnName("REVIEW_MODE_USER").HasColumnType("DECIMAL").HasMaxLength(1);
                entity.Property(e => e.CATALOG_CD).HasColumnName("CATALOG_CD").HasColumnType("DECIMAL").HasMaxLength(5);
                entity.Property(e => e.REVIEW_DATE).HasColumnName("REVIEW_DATE").HasColumnType("DATETIME");
                entity.Property(e => e.SCORE).HasColumnName("SCORE").HasColumnType("DECIMAL").HasMaxLength(3);
                entity.Property(e => e.COMMENT).HasColumnName("COMMENT").HasColumnType("NVARCHAR(500)").HasMaxLength(500);
                entity.Property(e => e.REG_DATETIME).HasColumnName("REG_DATETIME").HasColumnType("DATETIME");
                entity.Property(e => e.UPD_DATETIME).HasColumnName("UPD_DATETIME").HasColumnType("DATETIME");
            });

            builder.Entity<D_FAVOURITE>(entity =>
            {
                entity.ToTable("D_FAVOURITE");
                entity.HasKey(x => x.FAVOURITE_ID).HasName("D_FAVOURITE_PK");

                PropertyBuilder<decimal> id = entity.Property(e => e.FAVOURITE_ID);
                id.HasColumnName("FAVOURITE_ID");
                id.HasColumnType("DECIMAL");
                id.UseIdentityColumn();
                id.Metadata.SetBeforeSaveBehavior(PropertySaveBehavior.Ignore);

                entity.Property(e => e.FAVOURITE_ID).HasColumnName("FAVOURITE_ID").HasColumnType("DECIMAL").HasMaxLength(9);
                entity.Property(e => e.USER_ID).HasColumnName("USER_ID").HasColumnType("DECIMAL").HasMaxLength(9);
                entity.Property(e => e.MODE_USER).HasColumnName("MODE_USER").HasColumnType("DECIMAL").HasMaxLength(1);
                entity.Property(e => e.FAVOURITE_USER_ID).HasColumnName("FAVOURITE_USER_ID").HasColumnType("DECIMAL").HasMaxLength(9);
                entity.Property(e => e.CATALOG_CD).HasColumnName("CATALOG_CD").HasColumnType("DECIMAL").HasMaxLength(5);
                entity.Property(e => e.REG_DATETIME).HasColumnName("REG_DATETIME").HasColumnType("DATETIME");
            });

            builder.Entity<D_CONTACT>(entity =>
            {
                entity.ToTable("D_CONTACT");
                entity.HasKey(x => x.CONTACT_ID).HasName("D_CONTACT_PK");

                PropertyBuilder<decimal> id = entity.Property(e => e.CONTACT_ID);
                id.HasColumnName("CONTACT_ID");
                id.HasColumnType("DECIMAL");
                id.UseIdentityColumn();
                id.Metadata.SetBeforeSaveBehavior(PropertySaveBehavior.Ignore);

                entity.Property(e => e.CONTACT_ID).HasColumnName("CONTACT_ID").HasColumnType("DECIMAL").HasMaxLength(9);
                entity.Property(e => e.USER_RECIEVE_ID).HasColumnName("USER_RECIEVE_ID").HasColumnType("DECIMAL").HasMaxLength(9);
                entity.Property(e => e.CONTACT_USER_ID).HasColumnName("CONTACT_USER_ID").HasColumnType("DECIMAL").HasMaxLength(9);
                entity.Property(e => e.REG_MODE_USER).HasColumnName("REG_MODE_USER").HasColumnType("DECIMAL").HasMaxLength(1);
                entity.Property(e => e.CATALOG_CD).HasColumnName("CATALOG_CD").HasColumnType("DECIMAL").HasMaxLength(5);
                entity.Property(e => e.CONTACT_DATE).HasColumnName("CONTACT_DATE").HasColumnType("DATETIME");
            });

            builder.Entity<D_NOTIFY>(entity =>
            {
                entity.ToTable("D_NOTIFY");
                entity.HasKey(x => x.NOTIFY_ID).HasName("D_NOTIFY_PK");

                PropertyBuilder<decimal> id = entity.Property(e => e.NOTIFY_ID);
                id.HasColumnName("NOTIFY_ID");
                id.HasColumnType("DECIMAL");
                id.UseIdentityColumn();
                id.Metadata.SetBeforeSaveBehavior(PropertySaveBehavior.Ignore);

                entity.Property(e => e.NOTIFY_ID).HasColumnName("NOTIFY_ID").HasColumnType("DECIMAL").HasMaxLength(9);
                entity.Property(e => e.NOTIFY_TYPE).HasColumnName("NOTIFY_TYPE").HasColumnType("DECIMAL").HasMaxLength(1);
                entity.Property(e => e.OBJECT_ID).HasColumnName("OBJECT_ID").HasColumnType("DECIMAL").HasMaxLength(9);
                entity.Property(e => e.USER_SEND_ID).HasColumnName("USER_SEND_ID").HasColumnType("DECIMAL").HasMaxLength(9);
                entity.Property(e => e.USER_RECIEVE_ID).HasColumnName("USER_RECIEVE_ID").HasColumnType("DECIMAL").HasMaxLength(9);
                entity.Property(e => e.RECEIVE_MODE_USER).HasColumnName("RECEIVE_MODE_USER").HasColumnType("DECIMAL").HasMaxLength(1);
                entity.Property(e => e.NOTIFY_DATE).HasColumnName("NOTIFY_DATE").HasColumnType("DATETIME");
                entity.Property(e => e.CONTENT).HasColumnName("CONTENT").HasColumnType("NVARCHAR(500)").HasMaxLength(500);
                entity.Property(e => e.READED_FLG).HasColumnName("READED_FLG").HasColumnType("DECIMAL").HasMaxLength(1);
                entity.Property(e => e.REG_DATETIME).HasColumnName("REG_DATETIME").HasColumnType("DATETIME");
                entity.Property(e => e.UPD_DATETIME).HasColumnName("UPD_DATETIME").HasColumnType("DATETIME");
            });

            #endregion Table

            #region View

            builder.Entity<V_SUGGEST_JOB>(entity =>
            {
                entity.ToView("V_SUGGEST_JOB");

                entity.Property(e => e.SUGGEST_ID).HasColumnName("SUGGEST_ID").HasColumnType("DECIMAL").HasMaxLength(9);
                entity.Property(e => e.CATALOG_CD).HasColumnName("CATALOG_CD").HasColumnType("DECIMAL").HasMaxLength(5);
                entity.Property(e => e.CATALOG_NAME).HasColumnName("CATALOG_NAME").HasColumnType("NVARCHAR(100)").HasMaxLength(100);
                entity.Property(e => e.CATALOG_NAME_EN).HasColumnName("CATALOG_NAME_EN").HasColumnType("NVARCHAR(100)").HasMaxLength(100);
                entity.Property(e => e.TEMPLATE_CD).HasColumnName("TEMPLATE_CD").HasColumnType("DECIMAL").HasMaxLength(2);
                entity.Property(e => e.TEMPLATE_TITLE).HasColumnName("TEMPLATE_TITLE").HasColumnType("NVARCHAR(100)").HasMaxLength(100);
                entity.Property(e => e.TEMPLATE_TITLE_EN).HasColumnName("TEMPLATE_TITLE_EN").HasColumnType("NVARCHAR(100)").HasMaxLength(100);
                entity.Property(e => e.DISP_ORDER).HasColumnName("DISP_ORDER").HasColumnType("DECIMAL").HasMaxLength(3);
            });

            builder.Entity<V_JOB>(entity =>
            {
                entity.ToView("V_JOB");

                entity.Property(e => e.JOB_ID).HasColumnName("JOB_ID").HasColumnType("DECIMAL").HasMaxLength(9);
                entity.Property(e => e.USER_ID).HasColumnName("USER_ID").HasColumnType("DECIMAL").HasMaxLength(9);
                entity.Property(e => e.NAME).HasColumnName("NAME").HasColumnType("NVARCHAR(50)").HasMaxLength(50);
                entity.Property(e => e.PHONE_NUMBER).HasColumnName("PHONE_NUMBER").HasColumnType("NVARCHAR(20)").HasMaxLength(20);
                entity.Property(e => e.AVATAR).HasColumnName("AVATAR").HasColumnType("NVARCHAR(500)").HasMaxLength(500);
                entity.Property(e => e.TEMPLATE_CD).HasColumnName("TEMPLATE_CD").HasColumnType("DECIMAL").HasMaxLength(2);
                entity.Property(e => e.TEMPLATE_TITLE).HasColumnName("TEMPLATE_TITLE").HasColumnType("NVARCHAR(100)").HasMaxLength(100);
                entity.Property(e => e.TEMPLATE_TITLE_EN).HasColumnName("TEMPLATE_TITLE_EN").HasColumnType("NVARCHAR(100)").HasMaxLength(100);
                entity.Property(e => e.LATITUDE).HasColumnName("LATITUDE").HasColumnType("NVARCHAR(20)").HasMaxLength(20);
                entity.Property(e => e.LONGITUDE).HasColumnName("LONGITUDE").HasColumnType("NVARCHAR(20)").HasMaxLength(20);
                entity.Property(e => e.CATALOG_CD).HasColumnName("CATALOG_CD").HasColumnType("DECIMAL").HasMaxLength(5);
                entity.Property(e => e.CATALOG_NAME).HasColumnName("CATALOG_NAME").HasColumnType("NVARCHAR(50)").HasMaxLength(50);
                entity.Property(e => e.CATALOG_NAME_EN).HasColumnName("CATALOG_NAME_EN").HasColumnType("NVARCHAR(50)").HasMaxLength(50);
                entity.Property(e => e.CANCEL_FLG).HasColumnName("CANCEL_FLG").HasColumnType("DECIMAL").HasMaxLength(1);
                entity.Property(e => e.SCORE).HasColumnName("SCORE").HasColumnType("DECIMAL").HasMaxLength(3);
                entity.Property(e => e.LIKE_NUM).HasColumnName("LIKE_NUM").HasColumnType("DECIMAL").HasMaxLength(5);
                entity.Property(e => e.REG_DATETIME).HasColumnName("REG_DATETIME").HasColumnType("DATETIME");
                entity.Property(e => e.UPD_DATETIME).HasColumnName("UPD_DATETIME").HasColumnType("DATETIME");
            });

            builder.Entity<V_PARTNER>(entity =>
            {
                entity.ToView("V_PARTNER");

                entity.Property(e => e.USER_ID).HasColumnName("USER_ID").HasColumnType("DECIMAL").HasMaxLength(9);
                entity.Property(e => e.NAME).HasColumnName("NAME").HasColumnType("NVARCHAR(50)").HasMaxLength(50);
                entity.Property(e => e.AVATAR).HasColumnName("AVATAR").HasColumnType("NVARCHAR(MAX)");
                entity.Property(e => e.INTRODUCE).HasColumnName("INTRODUCE").HasColumnType("NVARCHAR(1000)").HasMaxLength(1000);

                entity.Property(e => e.CATALOG_CD).HasColumnName("CATALOG_CD").HasColumnType("DECIMAL").HasMaxLength(5);
                entity.Property(e => e.CATALOG_NAME).HasColumnName("CATALOG_NAME").HasColumnType("NVARCHAR(50)").HasMaxLength(50);
                entity.Property(e => e.CATALOG_NAME_EN).HasColumnName("CATALOG_NAME_EN").HasColumnType("NVARCHAR(50)").HasMaxLength(50);
                entity.Property(e => e.UNIT_CD).HasColumnName("UNIT_CD").HasColumnType("DECIMAL").HasMaxLength(5);
                entity.Property(e => e.UNIT_NAME).HasColumnName("UNIT_NAME").HasColumnType("NVARCHAR(50)").HasMaxLength(50);
                entity.Property(e => e.UNIT_NAME_EN).HasColumnName("UNIT_NAME_EN").HasColumnType("NVARCHAR(50)").HasMaxLength(50);
                entity.Property(e => e.COST).HasColumnName("COST").HasColumnType("DECIMAL").HasMaxLength(9);
                entity.Property(e => e.SCORE).HasColumnName("SCORE").HasColumnType("DECIMAL").HasMaxLength(3);
                entity.Property(e => e.MODE_USER).HasColumnName("MODE_USER").HasColumnType("DECIMAL").HasMaxLength(1);
            });

            builder.Entity<V_FAVOURITE>(entity =>
            {
                entity.ToView("V_FAVOURITE");

                entity.Property(e => e.FAVOURITE_ID).HasColumnName("FAVOURITE_ID").HasColumnType("DECIMAL").HasMaxLength(9);
                entity.Property(e => e.USER_ID).HasColumnName("USER_ID").HasColumnType("DECIMAL").HasMaxLength(9);
                entity.Property(e => e.MODE_USER).HasColumnName("MODE_USER").HasColumnType("DECIMAL").HasMaxLength(1);
                entity.Property(e => e.FAVOURITE_USER_ID).HasColumnName("FAVOURITE_USER_ID").HasColumnType("DECIMAL").HasMaxLength(9);
                entity.Property(e => e.REG_DATETIME).HasColumnName("REG_DATETIME").HasColumnType("DATETIME");
                entity.Property(e => e.PHONE_NUMBER).HasColumnName("PHONE_NUMBER").HasColumnType("NVARCHAR(20)").HasMaxLength(20);
                entity.Property(e => e.NAME).HasColumnName("NAME").HasColumnType("NVARCHAR(50)").HasMaxLength(50);
                //entity.Property(e => e.SEX).HasColumnName("SEX").HasColumnType("DECIMAL").HasMaxLength(1);
                //entity.Property(e => e.BIRTHDAY).HasColumnName("BIRTHDAY").HasColumnType("DATETIME");
                entity.Property(e => e.AVATAR).HasColumnName("AVATAR").HasColumnType("NVARCHAR(MAX)");
                entity.Property(e => e.INTRODUCE).HasColumnName("INTRODUCE").HasColumnType("NVARCHAR(1000)").HasMaxLength(1000);
                entity.Property(e => e.MODE_DEFAULT).HasColumnName("MODE_DEFAULT").HasColumnType("DECIMAL").HasMaxLength(1);
                //entity.Property(e => e.ACCOUNT_TYPE).HasColumnName("ACCOUNT_TYPE").HasColumnType("DECIMAL").HasMaxLength(1);
                //entity.Property(e => e.MEMBER_TYPE).HasColumnName("MEMBER_TYPE").HasColumnType("DECIMAL").HasMaxLength(1);
                entity.Property(e => e.CATALOG_CD).HasColumnName("CATALOG_CD").HasColumnType("DECIMAL").HasMaxLength(5);
                entity.Property(e => e.CATALOG_NAME).HasColumnName("CATALOG_NAME").HasColumnType("NVARCHAR(50)").HasMaxLength(50);
                entity.Property(e => e.COST).HasColumnName("COST").HasColumnType("DECIMAL").HasMaxLength(10);
                entity.Property(e => e.UNIT_CD).HasColumnName("UNIT_CD").HasColumnType("DECIMAL").HasMaxLength(5);
                entity.Property(e => e.UNIT_NAME).HasColumnName("UNIT_NAME").HasColumnType("NVARCHAR(50)").HasMaxLength(50);
                entity.Property(e => e.LIKE_NUM).HasColumnName("LIKE_NUM").HasColumnType("DECIMAL").HasMaxLength(9);
                entity.Property(e => e.SCORE).HasColumnName("SCORE").HasColumnType("DECIMAL").HasMaxLength(3);
            });

            builder.Entity<V_CONTACT_HISTORY>(entity =>
            {
                entity.ToView("V_CONTACT_HISTORY");

                entity.Property(e => e.CONTACT_ID).HasColumnName("CONTACT_ID").HasColumnType("DECIMAL").HasMaxLength(9);
                entity.Property(e => e.USER_RECIEVE_ID).HasColumnName("USER_RECIEVE_ID").HasColumnType("DECIMAL").HasMaxLength(9);
                entity.Property(e => e.CONTACT_USER_ID).HasColumnName("CONTACT_USER_ID").HasColumnType("DECIMAL").HasMaxLength(9);
                entity.Property(e => e.REG_MODE_USER).HasColumnName("REG_MODE_USER").HasColumnType("DECIMAL").HasMaxLength(1);
                entity.Property(e => e.CATALOG_CD).HasColumnName("CATALOG_CD").HasColumnType("DECIMAL").HasMaxLength(5);
                entity.Property(e => e.CONTACT_DATE).HasColumnName("CONTACT_DATE").HasColumnType("DATETIME");
                entity.Property(e => e.RECIEVE_USER_NAME).HasColumnName("RECIEVE_USER_NAME").HasColumnType("NVARCHAR(50)").HasMaxLength(50);
                entity.Property(e => e.CONTACT_USER_NAME).HasColumnName("CONTACT_USER_NAME").HasColumnType("NVARCHAR(50)").HasMaxLength(50);
            });

            builder.Entity<V_CONTACT_INFO>(entity =>
            {
                entity.ToView("V_CONTACT_INFO");

                entity.Property(e => e.CONTACT_ID).HasColumnName("CONTACT_ID").HasColumnType("DECIMAL").HasMaxLength(9);
                entity.Property(e => e.CONTACT_DATE).HasColumnName("CONTACT_DATE").HasColumnType("DATETIME");
                entity.Property(e => e.WORKER_ID).HasColumnName("WORKER_ID").HasColumnType("DECIMAL").HasMaxLength(9);
                entity.Property(e => e.WORKER_NAME).HasColumnName("WORKER_NAME").HasColumnType("NVARCHAR(50)").HasMaxLength(50);
                entity.Property(e => e.HIRER_ID).HasColumnName("HIRER_ID").HasColumnType("DECIMAL").HasMaxLength(9);
                entity.Property(e => e.HIRER_NAME).HasColumnName("HIRER_NAME").HasColumnType("NVARCHAR(50)").HasMaxLength(50);
                entity.Property(e => e.MODE_USER).HasColumnName("MODE_USER").HasColumnType("DECIMAL").HasMaxLength(1);
            });

            #endregion View
        }
    }
}