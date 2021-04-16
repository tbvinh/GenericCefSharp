using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data;
using System.ComponentModel.DataAnnotations.Schema;

namespace GenericCefSharp.Setting
{
    [Table("tbSetting")]
    public class clsSetting
    {
        [Column("id")]
        public int Id { get; set; }

        [Column("name")]
        public string Name { get; set; }

        [Column("val")]
        public string val { get; set; }

    }
}
