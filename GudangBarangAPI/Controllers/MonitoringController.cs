using Microsoft.AspNetCore.Mvc;
using GudangBarangAPI.Models;
using GudangBarangAPI.Models.Dto;
using GudangBarangAPI.Data;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using GudangBarangAPI.CommonResponse;
using GudangBarangAPI.Models.Dto;

namespace GudangBarangApi.Controllers
{
    /// <summary>
    /// This is class represents a monitoring.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]

    public class MonitoringController : ControllerBase
    {
        private readonly AppDbContext _context;

        /// <summary>
        /// Monitoring item of the warehouse.
        /// </summary>
        /// <param name="context"></param>
        public MonitoringController(AppDbContext context)
        {
            _context = context;
        }

        // Get Barang By Filter
        /// <summary>
        /// Get item by filter
        /// </summary>
        /// <param name="gudangName"></param>
        /// <param name="expiratedDate"></param>
        /// <returns>The get item by filter</returns>
        [HttpGet]
        public async Task<ActionResult<CommonResponse<IEnumerable<BarangDto>>>> GetBarangByFilter([FromQuery]string gudangName, [FromQuery] DateTime? expiratedDate)
        {
            var query = _context.Barang.Include(b => b.Gudang).AsQueryable();

            if (!string.IsNullOrEmpty(gudangName))
            {
                query = query.Where(b => b.Gudang.NamaGudang.Contains(gudangName));
            }

            if (expiratedDate.HasValue)
            {
                query = query.Where(b => b.ExpiredBarang == expiratedDate.Value);
            }

            var barangList = await query.ToListAsync();
            var barangDtoList = barangList.Select(b => new BarangDto
            {
                Id = b.Id,
                KodeBarang = b.KodeBarang,
                NamaBarang = b.NamaBarang,
                IDGudang = b.IDGudang,
                ExpiredBarang = b.ExpiredBarang,
                HargaBarang = b.HargaBarang,
                JumlahBarang = b.JumlahBarang,
                Gudang = new GudangDto
                {
                    Id = b.Gudang.Id,
                    KodeGudang = b.Gudang.KodeGudang,
                    NamaGudang = b.Gudang.NamaGudang
                }
            }).ToList();

            return Ok(new CommonResponse<IEnumerable<BarangDto>>
            {
                Status = new Status
                {
                    Code = 200,
                    Message = "OK"
                },
                Data = barangDtoList
            });
        }
    }
}