﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pars.Core.Data;

namespace Pars.Util.Puma.Data
{
    public partial class tb_UserLogonLog: EntityBase, IAuditable
    {
        public override int ObjectRef => this.UserLogonLogRef;

        [Key]
        public int UserLogonLogRef { get; set; }

        public short LogTypeRef { get; set; }

        public int UserLogonRef { get; set; }

        public int? UserRef { get; set; }

        public bool? IsDomainLogon { get; set; }

        [StringLength(255)]
        public string DomainName { get; set; }

        [StringLength(50)]
        public string UserName { get; set; }

        public short? PwdStateRef { get; set; }

        public short? LogonStateRef { get; set; }

        public DateTime? LastLogonDate { get; set; }

        public DateTime? LastPwdChangeDate { get; set; }

        public DateTime? PwdExpiryDate { get; set; }

        public int? InvalidLogonCount { get; set; }

        public int? PwdChangeCount { get; set; }

        public DateTime? ValidFrom { get; set; }

        public DateTime? ValidTo { get; set; }

        [StringLength(128)]
        public string PwdHash { get; set; }

        [Column(TypeName = "timestamp")]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        [MaxLength(8)]
        public byte[] RowVersion { get; set; }

        public int CreatedUserRef { get; set; }

        public DateTime CreatedDate { get; set; }

        public int ModifiedUserRef { get; set; }

        public DateTime ModifiedDate { get; set; }

        public Guid? SessionRef { get; set; }
    }
}
