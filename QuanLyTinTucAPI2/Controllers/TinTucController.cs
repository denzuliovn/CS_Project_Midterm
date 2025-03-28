using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace QuanLyTinTucAPI2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TinTucController : ControllerBase
    {
        private readonly AppDbContext _context;

        public TinTucController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/TinTuc
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TinTuc>>> GetTinTucs()
        {
            try
            {
                var tinTucs = await _context.TinTucs
                    .Include(tt => tt.NhanVien)
                    .Include(tt => tt.LoaiTinTuc)
                    .ToListAsync();
                return Ok(tinTucs);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Lỗi server: {ex.Message}");
            }
        }

        // GET: api/TinTuc/TT000001
        [HttpGet("{id}")]
        public async Task<ActionResult<TinTuc>> GetTinTuc(string id)
        {
            try
            {
                var tinTuc = await _context.TinTucs
                    .Include(tt => tt.NhanVien)
                    .Include(tt => tt.LoaiTinTuc)
                    .FirstOrDefaultAsync(tt => tt.MaTinTuc == id);

                if (tinTuc == null)
                {
                    return NotFound("Không tìm thấy tin tức.");
                }

                return Ok(tinTuc);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Lỗi server: {ex.Message}");
            }
        }

        // POST: api/TinTuc
        [HttpPost]
        public async Task<ActionResult<TinTuc>> CreateTinTuc(TinTuc tinTuc)
        {
            try
            {
                // Kiểm tra xem mã tin tức đã tồn tại chưa
                if (_context.TinTucs.Any(tt => tt.MaTinTuc == tinTuc.MaTinTuc))
                {
                    return BadRequest("Mã tin tức đã tồn tại.");
                }

                // Kiểm tra xem nhân viên và loại tin tức có tồn tại không
                if (!_context.NhanViens.Any(nv => nv.MaTaiKhoan == tinTuc.NguoiDangTin))
                {
                    return BadRequest("Nhân viên không tồn tại.");
                }

                if (!_context.LoaiTinTucs.Any(lt => lt.MaLoai == tinTuc.MaLoai))
                {
                    return BadRequest("Loại tin tức không tồn tại.");
                }

                // Đảm bảo NhanVien và LoaiTinTuc không được gửi trong JSON
                tinTuc.NhanVien = null;
                tinTuc.LoaiTinTuc = null;

                _context.TinTucs.Add(tinTuc);
                await _context.SaveChangesAsync();

                return CreatedAtAction(nameof(GetTinTuc), new { id = tinTuc.MaTinTuc }, tinTuc);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Lỗi server: {ex.Message}");
            }
        }

        // PUT: api/TinTuc/TT000001
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTinTuc(string id, TinTuc tinTuc)
        {
            try
            {
                if (id != tinTuc.MaTinTuc)
                {
                    return BadRequest("Mã tin tức không khớp.");
                }

                var existingTinTuc = await _context.TinTucs.FindAsync(id);
                if (existingTinTuc == null)
                {
                    return NotFound("Không tìm thấy tin tức.");
                }

                // Kiểm tra nhân viên và loại tin tức
                if (!_context.NhanViens.Any(nv => nv.MaTaiKhoan == tinTuc.NguoiDangTin))
                {
                    return BadRequest("Nhân viên không tồn tại.");
                }

                if (!_context.LoaiTinTucs.Any(lt => lt.MaLoai == tinTuc.MaLoai))
                {
                    return BadRequest("Loại tin tức không tồn tại.");
                }

                // Đảm bảo NhanVien và LoaiTinTuc không được gửi trong JSON
                tinTuc.NhanVien = null;
                tinTuc.LoaiTinTuc = null;

                _context.Entry(existingTinTuc).CurrentValues.SetValues(tinTuc);
                await _context.SaveChangesAsync();
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Lỗi server: {ex.Message}");
            }
        }

        // DELETE: api/TinTuc/TT000001
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTinTuc(string id)
        {
            try
            {
                var tinTuc = await _context.TinTucs.FindAsync(id);
                if (tinTuc == null)
                {
                    return NotFound("Không tìm thấy tin tức.");
                }

                _context.TinTucs.Remove(tinTuc);
                await _context.SaveChangesAsync();
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Lỗi server: {ex.Message}");
            }
        }
    }
}