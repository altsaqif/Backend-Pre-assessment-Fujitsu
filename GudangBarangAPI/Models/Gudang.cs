using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace GudangBarangAPI.Models
{
    /// <summary>
    /// Represents a warehouse.
    /// </summary>
    public class Gudang
    {
        /// <summary>
        /// Gets or sets the ID of the warehouse.
        /// </summary>
        [JsonPropertyName("id")]
        public int Id 
        {
            get; set;
        }

        /// <summary>
        /// Gets or sets the code of the warehouse
        /// </summary>
        [JsonPropertyName("kode_gudang")]
        public string KodeGudang
        {
            get; set;
        }

        /// <summary>
        /// Gets or sets the name of the warehouse.
        /// </summary>
        [JsonPropertyName("nama_gudang")]
        public string NamaGudang
        {
            get; set;
        }
    }
}