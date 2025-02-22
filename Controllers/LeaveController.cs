using Microsoft.AspNetCore.Mvc;
using AdminHRM.Server.Dtos;
using AdminHRM.Server.Services;
using AdminHRM.Services.Implements;
using AdminHRM.Dtos.Leaves;

namespace AdminHRM.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LeaveController : ControllerBase
    {
        private readonly ILogger<LeaveController> _logger;
        private readonly ILeaveServive _leaveService;

        public LeaveController(
            ILogger<LeaveController> logger,
            ILeaveServive leaveService)
        {
            _logger = logger;
            _leaveService = leaveService;
        }

        [HttpGet("GetLeaves")]
        public async Task<IActionResult> GetLeaves()
        {
            try
            {
                var data = await _leaveService.GetLeaveDtosAsync();
                return Ok(data);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("PostLeave")]
        public async Task<IActionResult> PostLeave(AdminHRM.Dtos.Leaves.LeaveCreateDto leaveCreateDto)
        {
            try
            {
                var data = await _leaveService.AddLeaveAsync(leaveCreateDto);
                return Ok(data);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("PutLeave")]
        public async Task<IActionResult> PutLeave(LeaveDto leaveDto)
        {
            try
            {
                var data = await _leaveService.EditLeaveAsync(leaveDto);
                if (data == null)
                {
                    return NotFound();
                }
                return Ok(data);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("DeleteLeave/{id}")]
        public async Task<IActionResult> DeleteLeave(Guid id)
        {
            try
            {
                var data = await _leaveService.RemoveLeaveDtosAsync(id);
                if (data == null)
                {
                    return NotFound();
                }
                return Ok(data);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("SearchLeaves")]
        public async Task<IActionResult> SearchLeaves([FromQuery] DateTime? fromDate,
            [FromQuery] DateTime? toDate,
            [FromQuery] string? leaveType,
            [FromQuery] string? leaveStatus,
            [FromQuery] string? employeeName,
            [FromQuery] string? subName)
        {
            try
            {
                var searchLeaveDto = new SearchLeaveDto
                {
                    FromDate = fromDate,
                    ToDate = toDate,
                    LeaveType = leaveType,
                    LeaveStatus = leaveStatus,
                    EmployeeName = employeeName,
                    SubName = subName
                };
                var data = await _leaveService.SearchLeaveDtosAsync(searchLeaveDto);
                return Ok(data);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("GetLeaveDashboardCards")]
        public async Task<IActionResult> GetLeaveDashboardCards()
        {
            try
            {
                var data = await _leaveService.GetDashboardCardsAsync();
                return Ok(data);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost("PostLeaveDashboardCard")]
        public async Task<IActionResult> PostLeaveDashboardCard(LeaveDashboardCreateDto leaveDashboardCreateDto)
        {
            try
            {
                var data = await _leaveService.AddDashboardCardsAsync(leaveDashboardCreateDto);
                return Ok(data);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("PutLeaveDashboardCard")]
        public async Task<IActionResult> PutLeaveDashboardCard(LeaveDashboardCreateDto leaveDashboardCreateDto)
        {
            try
            {
                var data = await _leaveService.EditDashboardCardsAsync(leaveDashboardCreateDto);
                if (data == null)
                {
                    return NotFound();
                }
                return Ok(data);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("DeleteLeaveDashboardCard/{cardId}")]
        public async Task<IActionResult> DeleteLeaveDashboardCard(string cardId)
        {
            try
            {
                var data = await _leaveService.RemoveDashboardCardsAsync(cardId);
                if (data == null)
                {
                    return NotFound();
                }
                return Ok(data);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("GetRequestReasons")]
        public async Task<IActionResult> GetRequestReasons()
        {
            try
            {
                var data = await _leaveService.GetRequestReasonAsync();
                return Ok(data);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost("PostRequestReason")]
        public async Task<IActionResult> PostRequestReason(LeaveReasonDto leaveReasonDto)
        {
            try
            {
                var data = await _leaveService.AddRequestReasonAsync(leaveReasonDto);
                return Ok(data);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("PutRequestReason")]
        public async Task<IActionResult> PutRequestReason(LeaveReasonDto leaveReasonDto)
        {
            try
            {
                var data = await _leaveService.EditRequestReasonAsync(leaveReasonDto);
                if (data == null)
                {
                    return NotFound();
                }
                return Ok(data);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("DeleteRequestReason/{reasonId}")]
        public async Task<IActionResult> DeleteRequestReason(string reasonId)
        {
            try
            {
                var data = await _leaveService.RemoveRequestReasonAsync(reasonId);
                if (data == null)
                {
                    return NotFound();
                }
                return Ok(data);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("GetRequestStatuses")]
        public async Task<IActionResult> GetRequestStatuses()
        {
            try
            {
                var data = await _leaveService.GetRequestStatusAsync();
                return Ok(data);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost("PostRequestStatus")]
        public async Task<IActionResult> PostRequestStatus(LeaveStatusDto leaveStatusDto)
        {
            try
            {
                var data = await _leaveService.AddRequestStatusAsync(leaveStatusDto);
                return Ok(data);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("PutRequestStatus")]
        public async Task<IActionResult> PutRequestStatus(LeaveStatusDto leaveStatusDto)
        {
            try
            {
                var data = await _leaveService.EditRequestStatusAsync(leaveStatusDto);
                if (data == null)
                {
                    return NotFound();
                }
                return Ok(data);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("DeleteRequestStatus/{statusId}")]
        public async Task<IActionResult> DeleteRequestStatus(string statusId)
        {
            try
            {
                var data = await _leaveService.RemoveRequestStatusAsync(statusId);
                if (data == null)
                {
                    return NotFound();
                }
                return Ok(data);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("v1/dashboard/cards")]
        public async Task<IActionResult> Cards()
        {
            try
            {
                var result = @"
                    [
                        {
                            ""type_id"": ""abnormal_case"",
                            ""type_name"": ""My Abnormal Case"",
                            ""total"": 13,
                            ""icon"": ""abc"",
                            ""display_order"": 1
                        },
                        {
                            ""type_id"": ""late_coming"",
                            ""type_name"": ""My Late Coming - Early Leave"",
                            ""total"": 13,
                            ""icon"": ""abc"",
                            ""display_order"": 2
                        },
                        {
                            ""type_id"": ""pending_request"",
                            ""type_name"": ""My Pending Request"",
                            ""total"": 12,
                            ""icon"": ""abc"",
                            ""display_order"": 3
                        },
                        {
                            ""type_id"": ""receive_request"",
                            ""type_name"": ""Receive Requests"",
                            ""total"": 10,
                            ""icon"": ""abc"",
                            ""display_order"": 4
                        }
                    ]";
                var jsonObject = System.Text.Json.JsonSerializer.Deserialize<object>(result);

                return Ok(jsonObject);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("v1/dashboard/cards/{type_id}")]
        public async Task<IActionResult> GetCardInfo()
        {
            try
            {
                var result = new
                {
                    total_count = 3,
                    page_index = 1,
                    page_size = 10,
                    items = new[]
                    {
                new
                {
                    date = "27-Dec-2024",
                    abnormal_type = "Leave Without Request",
                    request_type = "Nghỉ Phép",
                    request_status = "Submited",
                    partial_day = "Cả Ngày"
                },
                new
                {
                    date = "27-Dec-2024",
                    abnormal_type = "Leave Without Request",
                    request_type = "Nghỉ Phép",
                    request_status = "Submited",
                    partial_day = "Cả Ngày"
                },
                new
                {
                    date = "27-Dec-2024",
                    abnormal_type = "Leave Without Request",
                    request_type = "Nghỉ Phép",
                    request_status = "Submited",
                    partial_day = "Cả Ngày"
                }
            }
                };
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("v1/categories/requesttypes")]
        public async Task<IActionResult> RequestTypes()
        {
            try
            {
                var result = @"
                    [
                        {
                            ""type_id"": ""abnormal_case"",
                            ""type_name"": ""My Abnormal Case"",
                            ""display_order"": 1
                        },
                        {
                            ""type_id"": ""late_coming"",
                            ""type_name"": ""My Late Coming"",
                            ""display_order"": 2
                        }
                    ]";
                var jsonObject = System.Text.Json.JsonSerializer.Deserialize<object>(result);

                return Ok(jsonObject);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("v1/categories/statuses")]
        public async Task<IActionResult> RequestStatus()
        {
            try
            {
                var result = @"
                    [
                        {
                            ""status_id"": ""submit"",
                            ""status_name"": ""Submited"",
                            ""display_order"": 1
                        },
                        {
                            ""status_id"": ""un_submit"",
                            ""status_name"": ""UnSubmit"",
                            ""display_order"": 2
                        }
                    ]";
                var jsonObject = System.Text.Json.JsonSerializer.Deserialize<object>(result);

                return Ok(jsonObject);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("v1/myrequest")]
        public async Task<IActionResult> GetMyRequest()
        {
            try
            {
                var result = new
                {
                    total_count = 3,
                    page_index = 1,
                    page_size = 10,
                    items = new[]
                    {
                new
                {
                    request_no = 1,
                    request_type_name = "Nghỉ Phép",
                    time_from = "2024-11-22",
                    time_to = "2024-11-22",
                    time_request = "From...To...On...",
                    duration = 1.0,
                    reason = "Lý do..",
                    approver = "PM",
                    delegate_to = "Suppervisor",
                    status = "Submited"
                },
                new
                {
                    request_no = 2,
                    request_type_name = "Nghỉ Phép",
                    time_from = "2024-11-22",
                    time_to = "2024-11-22",
                    time_request = "From...To...On...",
                    duration = 1.0,
                    reason = "Lý do..",
                    approver = "PM",
                    delegate_to = "Suppervisor",
                    status = "Submited"
                },
                new
                {
                    request_no = 3,
                    request_type_name = "Nghỉ Phép",
                    time_from = "2024-11-22",
                    time_to = "2024-11-22",
                    time_request = "From...To...On...",
                    duration = 1.0,
                    reason = "Lý do..",
                    approver = "PM",
                    delegate_to = "Suppervisor",
                    status = "Submited"
                }
            }
                };
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("v1/categories/partialdays")]
        public async Task<IActionResult> RequestPartial()
        {
            try
            {
                var result = @"
                    [
                        {
                            ""partial_id"": ""full_day"",
                            ""partial_name"": ""Full Day"",
                            ""display_order"": 1
                        },
                        {
                            ""partial_id"": ""half_day"",
                            ""partial_name"": ""Half Day"",
                            ""display_order"": 2
                        }
                    ]";
                var jsonObject = System.Text.Json.JsonSerializer.Deserialize<object>(result);

                return Ok(jsonObject);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("v1/categories/reasons")]
        public async Task<IActionResult> RequestReason()
        {
            try
            {
                var result = @"
                    [
                        {
                            ""reason_id"": ""reason_1"",
                            ""reason_name"": ""WFH_HybridWorking"",
                            ""display_order"": 1
                        },
                        {
                            ""reason_id"": ""reason_2"",
                            ""reason_name"": ""Lý do sức khỏe cá nhân"",
                            ""display_order"": 2
                        }
                    ]";
                var jsonObject = System.Text.Json.JsonSerializer.Deserialize<object>(result);

                return Ok(jsonObject);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("v1/categories/approver")]
        public async Task<IActionResult> RequestApprovers()
        {
            try
            {
                var result = @"
                    [
                        {
                            ""user_id"": ""user_a"",
                            ""user_name"": ""Leader""
                        },
                        {
                             ""user_id"": ""user_b"",
                            ""user_name"": ""Project Manager""
                        }
                    ]";
                var jsonObject = System.Text.Json.JsonSerializer.Deserialize<object>(result);

                return Ok(jsonObject);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("v1/categories/suppervisors")]
        public async Task<IActionResult> RequestSuppervisors()
        {
            try
            {
                var result = @"
                    [
                        {
                            ""user_id"": ""user_a"",
                            ""user_name"": ""User_A""
                        },
                        {
                             ""user_id"": ""user_b"",
                            ""user_name"": ""User_B""
                        }
                    ]";
                var jsonObject = System.Text.Json.JsonSerializer.Deserialize<object>(result);

                return Ok(jsonObject);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("v1/categories/informto")]
        public async Task<IActionResult> RequestInformTo()
        {
            try
            {
                var result = @"
                    [
                        {
                            ""user_id"": ""user_c"",
                            ""user_name"": ""User_C""
                        },
                        {
                             ""user_id"": ""user_d"",
                            ""user_name"": ""User_D""
                        }
                    ]";
                var jsonObject = System.Text.Json.JsonSerializer.Deserialize<object>(result);

                return Ok(jsonObject);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("v1/categories/requesters")]
        public async Task<IActionResult> Requester()
        {
            try
            {
                var result = @"
                    [
                        {
                            ""user_id"": ""user_e"",
                            ""user_name"": ""User_E""
                        },
                        {
                             ""user_id"": ""user_f"",
                            ""user_name"": ""User_F""
                        }
                    ]";
                var jsonObject = System.Text.Json.JsonSerializer.Deserialize<object>(result);

                return Ok(jsonObject);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("v1/categories/requestTimeAdnBalances")]
        public async Task<IActionResult> RequestTimeAndBalance()
        {
            try
            {
                var result = @"[
            {
                ""requestName"": ""Nghỉ phép"",
                ""unit"": ""Day"",
                ""maximumAllowed"": 12,
                ""approvedQuotas"": 10.5,
                ""remainingQuotas"": 1.5,
                ""pendingQuotas"": 1
            },
            {
                ""requestName"": ""Nghỉ phép của năm trước"",
                ""unit"": ""Day"",
                ""maximumAllowed"": 0,
                ""approvedQuotas"": 1,
                ""remainingQuotas"": 0,
                ""pendingQuotas"": 0
            },
            {
                ""requestName"": ""Nghỉ bù"",
                ""unit"": ""Day"",
                ""maximumAllowed"": 0,
                ""approvedQuotas"": 0,
                ""remainingQuotas"": 0,
                ""pendingQuotas"": 0
            },
            {
                ""requestName"": ""Nghỉ ốm có giấy bệnh viện"",
                ""unit"": ""Day"",
                ""maximumAllowed"": 30,
                ""approvedQuotas"": 0,
                ""remainingQuotas"": 30,
                ""pendingQuotas"": 2
            },
            {
                ""requestName"": ""Nghỉ mất"",
                ""unit"": ""Day"",
                ""maximumAllowed"": 0,
                ""approvedQuotas"": 0,
                ""remainingQuotas"": 1,
                ""pendingQuotas"": 0
            },
            {
                ""requestName"": ""Nghỉ không lương"",
                ""unit"": ""Month"",
                ""maximumAllowed"": 1,
                ""approvedQuotas"": 0,
                ""remainingQuotas"": 1,
                ""pendingQuotas"": 0
            },
            {
                ""requestName"": ""Nghỉ kết hôn"",
                ""unit"": ""Day"",
                ""maximumAllowed"": 3,
                ""approvedQuotas"": 0,
                ""remainingQuotas"": 3,
                ""pendingQuotas"": 0
            },
            {
                ""requestName"": ""Nghỉ ốm điều trị dài ngày"",
                ""unit"": ""Day"",
                ""maximumAllowed"": 180,
                ""approvedQuotas"": 0,
                ""remainingQuotas"": 180,
                ""pendingQuotas"": 0
            },
            {
                ""requestName"": ""Nghỉ tang (từ thân phụ mẫu, vợ/chồng, con)"",
                ""unit"": ""Day"",
                ""maximumAllowed"": 3,
                ""approvedQuotas"": 0,
                ""remainingQuotas"": 3,
                ""pendingQuotas"": 0
            },
            {
                ""requestName"": ""Nghỉ do vợ sinh một (sinh thường)"",
                ""unit"": ""Day"",
                ""maximumAllowed"": 5,
                ""approvedQuotas"": 0,
                ""remainingQuotas"": 5,
                ""pendingQuotas"": 0
            },
            {
                ""requestName"": ""Nghỉ do vợ sinh một (phẫu thuật, hoặc sinh con dưới 32 tuần tuổi)"",
                ""unit"": ""Day"",
                ""maximumAllowed"": 7,
                ""approvedQuotas"": 0,
                ""remainingQuotas"": 7,
                ""pendingQuotas"": 0
            },
            {
                ""requestName"": ""Nghỉ do vợ sinh đôi (sinh thường)"",
                ""unit"": ""Day"",
                ""maximumAllowed"": 10,
                ""approvedQuotas"": 0,
                ""remainingQuotas"": 10,
                ""pendingQuotas"": 0
            },
            {
                ""requestName"": ""Nghỉ do vợ sinh ba (sinh thường)"",
                ""unit"": ""Day"",
                ""maximumAllowed"": 13,
                ""approvedQuotas"": 0,
                ""remainingQuotas"": 13,
                ""pendingQuotas"": 0
            },
            {
                ""requestName"": ""Nghỉ do vợ sinh đôi (phẫu thuật)"",
                ""unit"": ""Day"",
                ""maximumAllowed"": 14,
                ""approvedQuotas"": 0,
                ""remainingQuotas"": 14,
                ""pendingQuotas"": 0
            },
            {
                ""requestName"": ""Nghỉ nuôi dưỡng trẻ do người mẹ bị chết hoặc gặp rủi ro sau khi sinh"",
                ""unit"": ""Month"",
                ""maximumAllowed"": 6,
                ""approvedQuotas"": 0,
                ""remainingQuotas"": 6,
                ""pendingQuotas"": 0
            },
            {
                ""requestName"": ""Nghỉ nhận con nuôi dưới 6 tháng tuổi"",
                ""unit"": ""Month"",
                ""maximumAllowed"": 6,
                ""approvedQuotas"": 0,
                ""remainingQuotas"": 6,
                ""pendingQuotas"": 0
            },
            {
                ""requestName"": ""Nghỉ để thực hiện các biện pháp tránh thai"",
                ""unit"": ""Day"",
                ""maximumAllowed"": 15,
                ""approvedQuotas"": 0,
                ""remainingQuotas"": 15,
                ""pendingQuotas"": 0
            },
            {
                ""requestName"": ""Nghỉ dưỡng sức phục hồi sức khỏe sau ốm đau"",
                ""unit"": ""Day"",
                ""maximumAllowed"": 10,
                ""approvedQuotas"": 0,
                ""remainingQuotas"": 10,
                ""pendingQuotas"": 0
            },
            {
                ""requestName"": ""Holidays for expats"",
                ""unit"": ""Day"",
                ""maximumAllowed"": 0,
                ""approvedQuotas"": 0,
                ""remainingQuotas"": 0,
                ""pendingQuotas"": 0
            },
            {
                ""requestName"": ""Nghỉ khám Nghĩa vụ quân sự"",
                ""unit"": ""Day"",
                ""maximumAllowed"": 0,
                ""approvedQuotas"": 0,
                ""remainingQuotas"": 0,
                ""pendingQuotas"": 0
            }
        ]";
                var jsonObject = System.Text.Json.JsonSerializer.Deserialize<object>(result);

                return Ok(jsonObject);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("v1/receiverequest")]
        public async Task<IActionResult> GetReceiveRequest()
        {
            try
            {
                var result = new
                {
                    total_count = 3,
                    page_index = 1,
                    page_size = 10,
                    items = new[]
                    {
                new
                {
                    requester = "userAid",
                    request_type_name = "Nghỉ Phép",
                    time_request = "From...To...On...",
                    partial_days = "Full Day",
                    duration = 1.0, 
                    status = "Submited",
                    delegate_by = "SuppervisorAid",
                    delegate_to = "SuppervisorBid"
                },
                new
                {
                    requester = "userBid", 
                    request_type_name = "Nghỉ Bệnh", 
                    time_request = "From...To...On...", 
                    partial_days = "Half Day",
                    duration = 1.0,
                    status = "UnSubmit",
                    delegate_by = "SuppervisorAid",
                    delegate_to = "SuppervisorBid"
                },
                new
                {
                    requester = "userCid",
                    request_type_name = "Nghỉ Phép",
                    time_request = "From...To...On...",
                    partial_days = "Half Day",
                    duration = 1.0,
                    status = "Submited",
                    delegate_by = "SuppervisorAid",
                    delegate_to = "SuppervisorBid"
                }
            }
                };
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}