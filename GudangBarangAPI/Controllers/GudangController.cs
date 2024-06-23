using Microsoft.AspNetCore.Mvc;
using GudangBarangAPI.Models;
using GudangBarangAPI.Data;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using GudangBarangAPI.CommonResponse;

namespace GudangBarangAPI.Controllers
{
    /// <summary>
    /// This is class represents a warehouse.
    /// </summary>
    /// <param name="context"></param>
    [Route("api/[controller]")]
    [ApiController]
    public class GudangControler(AppDbContext context) : ControllerBase
    {
        private readonly AppDbContext _context = context;

        // Get All Data Gudang
        /// <summary>
        /// Get all value of items 
        /// </summary>
        /// <returns>The total value of items</returns>
        [HttpGet]
        public async Task<ActionResult<CommonResponse<IEnumerable<Gudang>>>> GetGudang([FromQuery] int page = 1, [FromQuery] int size = 10)
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

            var totalItems = await _context.Gudang.CountAsync();
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

            var gudangList = await _context.Gudang
                        .Skip((page - 1) * size)
                        .Take(size)
                        .ToListAsync();

            var response = new CommonResponse<IEnumerable<Gudang>>
            {
                Status = new Status
                {
                    Code = 200,
                    Message = "OK"
                },
                Data = gudangList,
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

        // Get Data Gudang By ID
        /// <summary>
        /// Get item by ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns>The get item by ID</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<CommonResponse<Gudang>>> GetGudang(int id)
        {
            var gudang = await _context.Gudang.FindAsync(id);
            if (gudang == null)
            {
                return NotFound(new CommonResponse<string>
                {
                    Status = new Status
                    {
                        Code = 404,
                        Message = "Gudang not found."
                    },
                    Data = null
                });
            }
            return Ok(new CommonResponse<Gudang>
            {
                Status = new Status
                {
                    Code = 200,
                    Message = "OK"
                },
                Data = gudang
            });
        }

        // Created Data Gudang
        /// <summary>
        /// Created item
        /// </summary>
        /// <param name="gudang"></param>
        /// <returns>The created item</returns>
        [HttpPost]
        public async Task<ActionResult<CommonResponse<Gudang>>> PostGudang(Gudang gudang)
        {
            if (!ModelState.IsValid)
            {
                foreach (var value in ModelState.Values)
                {
                    foreach (var error in value.Errors)
                    {
                        Console.WriteLine(error.ErrorMessage);
                    }
                }
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
            _context.Gudang.Add(gudang);
            await
            _context.SaveChangesAsync();
            return
            CreatedAtAction("GetGudang", new { id = gudang.Id }, new CommonResponse<Gudang>
            {
                Status = new Status
                {
                    Code = 201,
                    Message = "Created"
                },
                Data = gudang
            });
        }

        // Updated Data Gudang By ID
        /// <summary>
        /// Updated item by ID
        /// </summary>
        /// <param name="id"></param>
        /// <param name="gudang"></param>
        /// <returns>The updated item by ID</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutGudang(int id, [FromBody] Gudang gudang)
        {
            Console.WriteLine($"id parameter: {id}");
            Console.WriteLine($"gudang.Id: {gudang.Id}");
            if (id != gudang.Id)
            {
                return BadRequest(new CommonResponse<string>
                {
                    Status = new Status
                    {
                        Code = 400,
                        Message = "ID mismatch"
                    },
                    Data = null
                });
            }

            try
            {
            _context.Entry(gudang).State = EntityState.Modified;
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GudangExists(id))
                {
                    return NotFound(new CommonResponse<string>
                    {
                        Status = new Status
                        {
                            Code = 404,
                            Message = "Gudang not found"
                        },
                        Data = null
                    });
                }
                else
                {
                    throw;
                }
            }
            return Ok(new CommonResponse<Gudang>
            {
                Status = new Status
                {
                    Code = 200,
                    Message = "Gudang updated successfully."
                },
                Data = gudang
            });
        }

        // Deleted Data Gudang By ID
        /// <summary>
        /// Deleted item by ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns>The deleted item by ID</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGudang(int id)
        {
            var gudang = await _context.Gudang.FindAsync(id);
            if (gudang == null)
            {
                return NotFound(new CommonResponse<string>
                {
                    Status = new Status
                    {
                        Code = 404,
                        Message = "Gudang not found"
                    },
                    Data = null
                });
            }

            _context.Gudang.Remove(gudang);
            await
            _context.SaveChangesAsync();
            return Ok(new CommonResponse<Gudang>
            {
                Status = new Status
                {
                    Code = 200,
                    Message = "Gudang deleted successfully."
                },
                Data = gudang
            });
        }

        private bool GudangExists(int id)
        {
            return _context.Gudang.Any(e => e.Id == id);
        }
    }
}