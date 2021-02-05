using Pars.Core.Data;
using Pars.Core.Localization;

namespace Pars.Util.Puma.Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class tb_Application:EntityBase, ILocalizable, IAuditable
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tb_Application()
        {
            tb_Application_Locale = new HashSet<tb_Application_Locale>();
            tb_ApplicationMenu = new HashSet<tb_ApplicationMenu>();
            tb_MenuAction = new HashSet<tb_MenuAction>();
            tb_UserSetting = new HashSet<tb_UserSetting>();
        }

        [Key]
        public int ApplicationRef { get; set; }

        public Guid ApplicationGuid { get; set; }

        [StringLength(50)]
        public string Name { get; set; }

        public short? EntityStateRef { get; set; }

        [StringLength(512)]
        public string ReleaseDirectory { get; set; }

        [Column(TypeName = "timestamp")]
        [MaxLength(8)]
        [Timestamp]
        public byte[] RowVersion { get; set; }

        public int CreatedUserRef { get; set; }

        public DateTime CreatedDate { get; set; }

        public int ModifiedUserRef { get; set; }

        public DateTime ModifiedDate { get; set; }

        public Guid? SessionRef { get; set; }

        public virtual ICollection<tb_Application_Locale> tb_Application_Locale { get; set; }

        public virtual ICollection<tb_ApplicationMenu> tb_ApplicationMenu { get; set; }

        public virtual ICollection<tb_MenuAction> tb_MenuAction { get; set; }

        public virtual ICollection<tb_UserSetting> tb_UserSetting { get; set; }

        public override int ObjectRef => this.ApplicationRef;
    }
}
