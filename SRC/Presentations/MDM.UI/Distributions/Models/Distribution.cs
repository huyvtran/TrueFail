﻿using Common.Attributes;
using Common.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MDM.UI.Distributions.Models
{
    [Table("distribution")]
    public class Distribution : BaseModel
    {
        [Key]
        [Identity]
        [Column("id")]
        public int Id { set; get; }

        [Column("code")]
        public string Code { set; get; }

        [Column("name")]
        public string Name { set; get; }

        [Column("image_url")]
        public string ImageURL { set; get; }

        [Column("manager_id")]
        public int? ManagerId { set; get; }

        [Column("radius")]
        public double Radius { set; get; }

        [Column("address_id")]
        public int? AddressId { set; get; }

        [Column("contact_id")]
        public int? ContactId { set; get; }

        [Column("is_used")]
        public bool IsUsed { set; get; }
    }
}
