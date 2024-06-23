using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace GudangBarangAPI.Models
{
    /// <summary>
    /// Represents an item in the warehouse.
    /// </summary>
    public class Barang
    {
        /// <summary>
        /// Gets or sets the ID of the item.
        /// </summary>
        [JsonPropertyName("id")]
        public int Id 
        {
            get; set;
        }

        /// <summary>
        /// Gets or sets the code of the item.
        /// </summary>
        [JsonPropertyName("kode_barang")]
        public string KodeBarang
        {
            get; set;
        }

        /// <summary>
        /// Gets or sets the warehouse code that this item belongs to.
        /// </summary>
        [JsonPropertyName("id_gudang")]
        public int IDGudang
        {
            get; set;
        }

        /// <summary>
        /// Gets or sets the name of the item.
        /// </summary>
        [JsonPropertyName("nama_barang")]
        public string NamaBarang
        {
            get; set;
        }

        /// <summary>
        /// Gets or sets the price of the item.
        /// </summary>
        [JsonPropertyName("harga_barang")]
        public decimal HargaBarang
        {
            get; set;
        }

        /// <summary>
        /// Gets or sets the quantity of the item.
        /// </summary>
        [JsonPropertyName("jumlah_barang")]
        public int JumlahBarang
        {
            get; set;
        }

        /// <summary>
        /// Gets or sets the expiration date of the item.
        /// </summary>
        [JsonPropertyName("expired_barang")]
        public required DateTime ExpiredBarang
        {
            get; set;
        }

        /// <summary>
        /// Gets or sets the warehouse that this item belongs to.
        /// </summary>

        [JsonIgnore]
        [ValidateNever]
        public Gudang Gudang
        {
            get; set;
        }
    }
}