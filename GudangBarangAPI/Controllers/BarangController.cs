using Microsoft.AspNetCore.Mvc;
using GudangBarangAPI.Models;
using GudangBarangAPI.Data;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using GudangBarangAPI.Models.Dto;
using GudangBarangAPI.CommonResponse;

namespace GudangBarangApi.Controllers
{
    /// <summary>
    /// This class represents a item.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]

    public class BarangController : ControllerBase
    {
        private readonly AppDbContext _context;

        /// <summary>
        /// Adds a new context to the item.
        /// </summary>
        /// <param name="context"></param>
        public BarangController(AppDbContext context)
        {
            _context = context;
        }

        // Get All Barang
        /// <summary>
        /// Get all value of items
        /// </summary>
        /// <returns>The total value of items</returns>
        [HttpGet]
        public async Task<ActionResult<CommonResponse<IEnumerable<BarangDto>>>> GetBarang([FromQuery] int page = 1, [FromQuery] int size = 10)
        {
            if (page <= 0 || size <= 0)
            {
                return BadRequest(new CommonResponse<string>
                {
                    Status = new Status
                    {
                        Code = 400,
                        Message = "Page and size must be greater than 0."
                    },
                    Data = null
                });
            }

            var totalItems = await _context.Barang.CountAsync();
            var totalPages = (int)Math.Ceiling(totalItems / (double)size);

            if (page > totalPages)
            {
                return BadRequest(new CommonResponse<string>
                {
                    Status = new Status
                    {
                        Code = 400,
                        Message = "Page number exceeds total pages."
                    },
                    Data = null
                });
            }

            var barangList = await _context.Barang.Include(b => b.Gudang)
                                                .Skip((page - 1) * size)
                                                .Take(size)
                                                .ToListAsync();

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

            var response = new CommonResponse<IEnumerable<BarangDto>>
            {
                Status = new Status
                {
                    Code = 200,
                    Message = "OK"
                },
                Data = barangDtoList,
                Paging = new Paging
                {
                    CurrentPage = page,
                    PageSize = size,
                    TotalPages = totalPages,
                    TotalItems = totalItems
                }
            };

            return Ok(response);
        }

        // Get Barang By ID
        /// <summary>
        /// Get item by ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns>The item by ID</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<CommonResponse<Barang>>> GetBarang(int id)
        {
            Console.WriteLine($"Requested id: {id}");
            Console.WriteLine("Executing database query...");
            var barang = await _context.Barang.Include(b => b.Gudang).FirstOrDefaultAsync(b => b.Id == id);
            if (barang == null)
            {
                Console.WriteLine("Item not found.");
                return NotFound(new CommonResponse<string>
                {
                    Status = new Status
                    {
                        Code = 404,
                        Message = "Item not found."
                    },
                    Data = null
                });
            }
            Console.WriteLine("Item found.");
            return Ok(new CommonResponse<Barang>
            {
                Status = new Status
                {
                    Code = 200,
                    Message = "OK"
                },
                Data = barang
            });
        }

        // Created Barang
        /// <summary>
        /// Created item
        /// </summary>
        /// <param name="barang"></param>
        /// <returns>The created item</returns>
        [HttpPost]
        public async Task<ActionResult<CommonResponse<Barang>>> PostBarang(Barang barang)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new CommonResponse<string>
                {
                    Status = new Status
                    {
                        Code = 400,
                        Message = "Invalid data."
                    },
                    Data = null
                });
            }

            _context.Barang.Add(barang);
            await
            _context.SaveChangesAsync();
            return CreatedAtAction("GetBarang", new { id = barang.Id }, new CommonResponse<Barang>
            {
                Status = new Status
                {
                    Code = 201,
                    Message = "Created"
                },
                Data = barang
            });
        }

        // Updated Barang By ID
        /// <summary>
        /// Updated item by ID
        /// </summary>
        /// <param name="id"></param>
        /// <param name="barang"></param>
        /// <returns>The updated item by ID</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBarang(int id, [FromBody] Barang barang)
        {
            if (id != barang.Id)
            {
                return BadRequest(new CommonResponse<string>
                {
                    Status = new Status
                    {
                        Code = 400,
                        Message = "ID mismatch."
                    },
                    Data = null
                });
            }

            try
            {
            _context.Entry(barang).State = EntityState.Modified;
                await
                _context.SaveChangesAsync();
            }
            catch
            (DbUpdateConcurrencyException)
            {
                if (!BarangExists(id))
                {
                    return NotFound(new CommonResponse<string>
                    {
                        Status = new Status
                        {
                            Code = 404,
                            Message = "Item not found."
                        },
                        Data = null
                    });
                }
                else
                {
                    throw;
                }
            }
            return Ok(new CommonResponse<Barang>
            {
                Status = new Status
                {
                    Code = 200,
                    Message = "Item has been updated."
                },
                Data = barang
            });
        }

        // Delete Barang By ID
        /// <summary>
        /// Deleted item by ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns>The deleted item by ID</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBarang(int id)
        {
            var barang = await _context.Barang.FindAsync(id);
            if (barang == null)
            {
                return NotFound(new CommonResponse<string>
                {
                    Status = new Status
                    {
                        Code = 404,
                        Message = "Item not found."
                    },
                    Data = null
                });
            }

            _context.Barang.Remove(barang);
            await
            _context.SaveChangesAsync();
            return Ok(new CommonResponse<Barang>
            {
                Status = new Status
                {
                    Code = 200,
                    Message = "Item has been deleted."
                },
                Data = barang
            });
        }
        private bool BarangExists(int id)
        {
            return _context.Barang.Any(e => e.IDGudang == id);
        }
    }
}
