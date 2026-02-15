<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TourPlanApps.aspx.cs" Inherits="TourPlanApps" %>

<!DOCTYPE html>
<html lang="en">
<head runat="server">
    <meta charset="UTF-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0"/>
    <title>Tour Plan (Work Schedule)</title>

    <!-- CDN -->
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.4.0/css/all.min.css"/>
 <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.3/dist/css/bootstrap.min.css" rel="stylesheet">

    <script src="https://code.jquery.com/jquery-3.6.0.min.js"></script>
     
    
<script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.3/dist/js/bootstrap.bundle.min.js"></script>

    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/toastr.js/latest/toastr.min.css"/>
<script src="https://cdnjs.cloudflare.com/ajax/libs/toastr.js/latest/toastr.min.js"></script>


    <style>
        .final-btn {
    background-color: #28a745!important; /* সবুজ */
    color: white!important;
}
        * { margin:0; padding:0; box-sizing:border-box; font-family:'Segoe UI', Tahoma, Geneva, Verdana, sans-serif; }
      
        .header { text-align:center; padding:5px; color:#fff; margin-bottom:10px; }
        .container-wrap { display:flex; flex-direction:column; max-width:1200px; margin:0 auto; background:#fff; border-radius:15px; overflow:hidden; box-shadow:0 15px 30px rgba(0,0,0,.2); flex-grow:1; }
        .controls { display:flex; justify-content:space-between; align-items:center; padding:20px; background:#f8f9fa; border-bottom:1px solid #e0e0e0; flex-wrap:wrap; }
        .date-selectors { display:flex; gap:15px; }
        .select-group { display:flex; flex-direction:column; }
        .select-group label { margin-bottom:5px; font-weight:600; color:#2d3748; font-size:.9rem; }
        select { padding:10px 15px; border:1px solid #cbd5e0; border-radius:8px; font-size:1rem; background:#fff; cursor:pointer; min-width:120px; }

        .calendar-container { padding:2px; flex-grow:1; display:flex; flex-direction:column; }
        .month-year { text-align:center; font-size:1.0rem; margin-bottom:5px; color:#2d3748; font-weight:600; }
        .vertical-calendar { display:flex; flex-direction:column; gap:10px; max-width:100%; margin:0 auto; width:100%; }

        .calendar-item { display:flex; padding:15px; background:#f8f9fa; border-radius:10px; transition:.3s; border:1px solid #e2e8f0; margin-bottom:15px; }
        .calendar-item:hover { background:#FFFFF0; transform:translateX(5px); box-shadow:0 5px 15px rgba(0,0,0,.1); }
        .date-info { display:flex; flex-direction:column; min-width:80px; padding-right:20px; border-right:1px solid #e2e8f0; }
        .date-number { font-size:1.2rem; font-weight:500; text-align:center; color:#2d3748; margin-bottom:2px; }
        .day-info { text-align:center; }
        .day-name { font-size:0.7rem; color:#4b6cb7; font-weight:400; margin-bottom:2px; }

        .date-table { flex-grow:1; margin-left:0px; }
        .date-table table { width:100%; border-collapse:collapse; }
        .date-table th { background:#f1f5f9; padding:4px; text-align:center; font-weight:300; border:1px solid #e2e8f0; font-size:.7rem; }
        .date-table td { padding:3px; border:1px solid #e2e8f0; text-align:center; font-size:.7rem; }

        .current-day { background:#E6E6FA; color:#000; }

        .footer { text-align:center; padding:20px; background:#f8f9fa; border-top:1px solid #e0e0e0; color:#718096; }

        @media (max-width: 992px) {
            .calendar-item { flex-direction:column; }
            .date-info { border-right:none; border-bottom:1px solid #e2e8f0; padding-right:0; padding-bottom:15px; margin-bottom:15px; min-width:auto; }
            .date-table { margin-left:0; }
        }
        @media (max-width: 768px) {
            .controls { flex-direction:column; gap:15px; }
            .date-selectors { width:100%; justify-content:center; }
            .vertical-calendar { max-width:100%; }
            .date-table { overflow-x:auto; }
        }

        .submit-section { padding:30px; background:#f8f9fa; border-top:1px solid #e0e0e0; text-align:center; }
        .submit-btn { background:linear-gradient(135deg,#6a11cb 0%,#2575fc 100%); color:#fff; border:none; padding:15px 40px; font-size:1.1rem; font-weight:600; border-radius:50px; cursor:pointer; transition:.3s; box-shadow:0 5px 15px rgba(0,0,0,.2); }
        .submit-btn:hover { transform:translateY(-3px); box-shadow:0 8px 20px rgba(0,0,0,.3); }

        .success-message { text-align:center; padding:20px; color:#2d8748; background:#d4edda; border-radius:8px; margin-top:20px; display:none; }
        .error-message { text-align:center; margin:10px 0; display:none; }

        /* ===== Modal-like Loading Overlay ===== */
        .loading-overlay {
          position: fixed;
          inset: 0;
          background: rgba(0,0,0,0.45);
          display: none;
          align-items: center;
          justify-content: center;
          z-index: 1050;
          backdrop-filter: blur(2px);
        }
        .loading-overlay.active { display: flex; }
        .loading-box {
          background: #ffffff;
          border-radius: 14px;
          padding: 24px 28px;
          box-shadow: 0 10px 30px rgba(0,0,0,0.25);
          text-align: center;
          min-width: 240px;
        }
        .loading-box i { font-size: 28px; margin-bottom: 10px; display: block; color: #4b6cb7; }
        .loading-title { font-weight: 600; margin-bottom: 4px; color: #2d3748; }
        .loading-sub { font-size: 12px; color: #6b7280; }
    </style>
</head>
<body>
<form id="form1" runat="server">
  

    <div class="container-wrap">
        <div class="controls">
            <div class="date-selectors">
                <div class="select-group">
                    <label for="year-select">Year</label>
                    <select id="year-select"></select>
                </div>
                <div class="select-group">
                    <label for="month-select">Month</label>
                    <select id="month-select">
                        <option value="0">January</option>
                        <option value="1">February</option>
                        <option value="2">March</option>
                        <option value="3">April</option>
                        <option value="4">May</option>
                        <option value="5">June</option>
                        <option value="6">July</option>
                        <option value="7">August</option>
                        <option value="8">September</option>
                        <option value="9">October</option>
                        <option value="10">November</option>
                        <option value="11">December</option>
                    </select>
                </div>
            </div>

            <div class="row w-100 mt-3">
                <div class="col-md-12">
                    <div class="form-group">
                        <label class="font-weight-bold">Employee:</label>
                        <span class="font-weight-bold" id="employeeInput"></span>
                    </div>
                </div>

                <div class="col-md-4">
                    <div class="form-group">
                        <label class="font-weight-bold">Company:</label>
                        <span id="companyInput"></span>
                    </div>
                    <div class="form-group">
                        <label class="font-weight-bold">Region:</label>
                        <span id="regionInput"></span>
                    </div>
                </div>
                              <%--      <div class="form-group">
                        <label class="font-weight-bold">SL:</label>
                        <span id="slInput"></span>
                    </div>--%>
                <div class="col-md-4">

                    <div class="form-group">
                        <label class="font-weight-bold">Area:</label>
                        <span id="areaInput"></span>
                    </div>
                </div>

                <div class="col-md-4">
                    <div class="form-group">
                        <label class="font-weight-bold">Zone:</label>
                        <span id="zoneInput"></span>
                    </div>
                    <div class="form-group">
                        <label class="font-weight-bold">Territory:</label>
                        <span id="territoryInput"></span>
                    </div>
                </div>

                <!-- Inside each calendar item -->
<div class="col-md-4">
  <div class="form-group">
    <small class="text-muted d-block mb-1">Approval Status</small>
    <div class="approval-row">
      <span class="me-2"></span>
      <!-- keep this if you already have it; duplicate IDs are okay because we scope via $item.find(...) -->
      <span id="approval-morning"></span>
      <!-- (Better) use a class to avoid duplicate IDs:
      <span class="approval-morning"></span>
      -->
    </div>
  </div>
</div>

            </div>
        </div>

   
        <div class="calendar-container">
            <div class="month-year" id="month-year">Month Year</div>

            <!-- পুরনো ছোট লোডিং (চাইলে রেখে দিন; ওভারলে-ই ব্যবহার হবে) -->
            <div id="loading" class="loading" style="display:none;">
                <i class="fas fa-spinner fa-spin"></i>
                <p>Loading ...</p>
            </div>

            <div id="errorMessage" class="error-message text-danger">
                <i class="fas fa-exclamation-circle"></i>
                <p>An error occurred while fetching data. Please try again.</p>
            </div>

            <div class="vertical-calendar" id="vertical-calendar"></div>

            <div class="submit-section">
                <div id="successMessage" class="success-message" style="margin-bottom:10px">
    <i class="fas fa-check-circle"></i> Your tour plan has been submitted successfully!
</div>
    <!-- Normal Submit Button -->
    <button id="submitBtn" class="submit-btn" type="button">
        <i class="fas fa-paper-plane"></i> Save
    </button>

    <!-- New Final Submit Button -->
    <button id="finalSubmitBtn" class="submit-btn final-btn" type="button" style="margin:10px; ">
        <i class="fas fa-lock"></i> Final Submit
    </button>

    
</div>

        </div>

        <div class="footer" style="display:none">
            <p>© <span id="currentYear"></span> CSTL <i class="fas fa-heart" style="color:#e74c3c;"></i> | <span id="currentDateTime"></span></p>
        </div>
    </div>
</form>

<!-- ===== Modal-like Loading Overlay ===== -->
<div id="loadingOverlay" class="loading-overlay" aria-hidden="true">
  <div class="loading-box">
    <i class="fas fa-spinner fa-spin"></i>
    <div class="loading-title" id="loadingTitle">Loading…</div>
    <div class="loading-sub" id="loadingSub">Please wait</div>
  </div>
</div>


    <!-- Final Submit Confirmation Modal -->
<div class="modal fade" id="finalConfirmModal" tabindex="-1" aria-labelledby="finalConfirmLabel" aria-hidden="true">
  <div class="modal-dialog">
    <div class="modal-content">
      <div class="modal-header">
        <h5 class="modal-title" id="finalConfirmLabel"><i class="fas fa-lock"></i> Final Submit Confirmation</h5>
        <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
      </div>

      <div class="modal-body">
      <!-- Overall totals (আগেরটা যেমন ছিল) -->
<div class="d-flex justify-content-between mb-2">
  <div><strong>HQ:</strong> <span id="totHQ">0</span></div>
  <div><strong>EQ:</strong> <span id="totEQ">0</span></div>
  <div><strong>OS:</strong> <span id="totOS">0</span></div>
</div>

<!-- NEW: Morning vs Evening breakdown -->
<div class="mb-2">
  <table class="table table-sm table-bordered mb-0">
    <thead>
      <tr>
        <th style="width:120px;"></th>
        <th class="text-center">Morning</th>
        <th class="text-center">Evening</th>
      </tr>
    </thead>
    <tbody>
      <tr>
        <td><strong>HQ</strong></td>
        <td class="text-center" id="totM_HQ">0</td>
        <td class="text-center" id="totE_HQ">0</td>
      </tr>
      <tr>
        <td><strong>EQ</strong></td>
        <td class="text-center" id="totM_EQ">0</td>
        <td class="text-center" id="totE_EQ">0</td>
      </tr>
      <tr>
        <td><strong>OS</strong></td>
        <td class="text-center" id="totM_OS">0</td>
        <td class="text-center" id="totE_OS">0</td>
      </tr>
      <tr class="table-active">
        <td><strong>Total Visits</strong></td>
        <td class="text-center" id="totM_Total">0</td>
        <td class="text-center" id="totE_Total">0</td>
      </tr>
    </tbody>
  </table>
</div>

        <div class="mb-2">
          <label for="finalRemarks" class="form-label">Remarks  <span style="color:red">*</span></label>
          <textarea id="finalRemarks" class="form-control" rows="3" placeholder="Add remarks for final submission..."></textarea>
        </div>

        <div class="alert alert-warning mb-0">
          <i class="fas fa-exclamation-triangle"></i>
          After final submit, past dates will remain locked and further edits may be restricted.
        </div>
      </div>

      <div class="modal-footer">
        <button type="button" id="cancelFinalBtn"  class="btn btn-outline-secondary" data-bs-dismiss="modal">Cancel</button>
        <button type="button" id="confirmFinalSubmitBtn" class="btn btn-info">
          <i class="fas fa-check-circle"></i> Confirm Final Submit
        </button>
      </div>
    </div>
  </div>
</div>


<script type="text/javascript">
    // ---------- Utils ----------

    toastr.options = {
        closeButton: true,
        progressBar: true,
        positionClass: "toast-top-right",
        timeOut: 2000,
        extendedTimeOut: 1000,
        newestOnTop: true,
        preventDuplicates: true
    };
     window.openFinalModal = function () {
        const el = document.getElementById('finalConfirmModal');
        if (!el) return console.error('finalConfirmModal not found');
        bootstrap.Modal.getOrCreateInstance(el, { backdrop: 'static', keyboard: false }).show();
    };

    window.closeFinalModal = function () {
        const el = document.getElementById('finalConfirmModal');
        if (!el) return;
        bootstrap.Modal.getOrCreateInstance(el).hide();
    };

    let empId_MAin=0 ;
    function pad(n) { return n < 10 ? '0' + n : '' + n; }
    function formatDateYMD(year, monthIndex, day) { return year + '-' + pad(monthIndex + 1) + '-' + pad(day); }
    function getCurrentDateTime() {
        var options = { weekday: 'long', year: 'numeric', month: 'long', day: 'numeric', hour: '2-digit', minute: '2-digit', second: '2-digit', timeZone: 'Asia/Dhaka', timeZoneName: 'short' };
        var currentDate = new Date().toLocaleString('en-US', options);
        document.getElementById('currentDateTime').textContent = currentDate;
        document.getElementById('currentYear').textContent = new Date().getFullYear();
    }
    function showLoader(title, sub) { $('#loadingTitle').text(title || 'Loading…'); $('#loadingSub').text(sub || 'Please wait'); $('#loadingOverlay').addClass('active'); }
    function hideLoader() { $('#loadingOverlay').removeClass('active'); }

    // ---------- State ----------
    let tourTypes = [];     // expects JSON array (response.d is stringified JSON)
    let SubTerritorys = []; // expects JSON array (response.d is stringified JSON)

    // ---------- Ready ----------
    $(document).ready(function () {
        getCurrentDateTime();

        // year 2020-2030
        for (let y = 2020; y <= 2030; y++) { $('#year-select').append($('<option>', { value: y, text: y })); }
        const now = new Date();
        $('#year-select').val(now.getFullYear());
        $('#month-select').val(now.getMonth());

        // initial calendar
        generateVerticalCalendar(parseInt($('#month-select').val(), 10), parseInt($('#year-select').val(), 10));

        // events
        $('#year-select').on('change', function () { generateVerticalCalendar(parseInt($('#month-select').val(), 10), parseInt($('#year-select').val(), 10)); });
        $('#month-select').on('change', function () { generateVerticalCalendar(parseInt($('#month-select').val(), 10), parseInt($('#year-select').val(), 10)); });
        $('#submitBtn').on('click', function () {
            submitTourPlan('draft','');   // type = draft / normal submit
        });

        // Final Submit
        //$('#finalSubmitBtn').on('click', function () {
        //    submitTourPlan('final');   // type = final submit
        //});


        // load data
        const urlParams = new URLSearchParams(window.location.search);
        const empId = urlParams.get('EmpId');
        empId_MAin = empId;
        $(document).one('employeeData:loaded', function () {
            setTimeout(function () {
                loadTourTypes();
            }, 3000);
        });

        // আগেরটা থেকে loadTourTypes() সরিয়ে দিন
        loadEmployeeData(empId);
    });

    document.getElementById('cancelFinalBtn')
        .addEventListener('click', function (e) {
            e.preventDefault();
            closeFinalModal();
        });
    $('#cancelFinalBtn').on('click', function (e) {
        e.preventDefault();
        closeFinalModal();
    });
 

    // ---------- AJAX (keep original shapes) ----------
    function loadTourTypes() {
        $('#loading').show(); showLoader('Loading tour types…', 'Fetching list');
        setTimeout(function () {
            try {
                $.ajax({
                    type: "POST",
                    url: "TourPlanApps.aspx/GetTourTypeList",
                    data: "{}",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (response) {
                        // response.d is a JSON string (per your original code)
                        tourTypes = JSON.parse(response.d || "[]");
                        populateTourTypeDropdowns();
                        $('#loading').hide(); hideLoader();
                    },
                    error: function () {
                        $('#loading').hide(); hideLoader();
                        // keep your behavior (silent or show error)
                    }
                });
            } catch (e) {
                $('#loading').hide(); hideLoader();
            }
        }, 500);
    }

    function loadSubTerritory(STerritoryId, RoleType) {
        if (!STerritoryId) return;
        $('#loading').show(); showLoader('Loading territories…', 'Applying region scope');
        $('#errorMessage').hide();
        setTimeout(function () {
            try {
                $.ajax({
                    type: "POST",
                    url: "TourPlanApps.aspx/GetSubTerritoryList",
                    data: JSON.stringify({ STerritoryId: STerritoryId, RoleType: RoleType }),
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (response) {
                        // response.d is a JSON string (per your original code)
                        SubTerritorys = JSON.parse(response.d || "[]");
                        populateSubTerritoryDropdowns();
                        $('#loading').hide(); hideLoader();
                    },
                    error: function () {
                        $('#loading').hide(); hideLoader();
                        // $('#errorMessage').show();
                    }
                });
            } catch (e) {
                $('#loading').hide(); hideLoader();
                // $('#errorMessage').show();
            }
        }, 500);
    }

    function loadEmployeeData(empId) {
        $('#loading').show(); showLoader('Loading employee data…', 'Preparing your hierarchy');
        $('#errorMessage').hide();
        setTimeout(function () {
            try {
                $.ajax({
                    url: '/TourPlanApps.aspx/GetEmployeeHierarchy',
                    type: 'POST',
                    data: JSON.stringify({ empId: empId }),
                    contentType: 'application/json; charset=utf-8',
                    dataType: 'json',
                    success: function (response) {

                        $(document).trigger('employeeData:loaded');
                        // per your original: response.d.success + response.d.data
                        if (response.d && response.d.success) {
                            populateForm(response.d.data);
                            if (response.d.data && response.d.data.STerritoryId) {
                             //   loadSubTerritory(response.d.data.STerritoryId);
                            } else {
                                $('#loading').hide(); hideLoader();
                            }
                        } else {
                            $('#loading').hide(); hideLoader();
                            alert(response.d ? (response.d.message || 'Failed to load employee data.') : 'Failed to load employee data.');
                        }
                    },
                    error: function () {
                        $('#loading').hide(); hideLoader();
                        alert("Failed to load employee data.");
                    }
                });
            } catch (e) {
                $('#loading').hide(); hideLoader();
            }
        }, 500);
    }

    // ---------- Populate top info ----------
    function populateForm(data) {
        if (!data) return;

         
        // keep your exact text formats
        $('#companyInput').text(`${data.GrpCo} : ${data.GrpName}`);
        $('#regionInput').text(`${data.RegionCo} : ${data.RegionName}`);
        $('#employeeInput').text(`${data.EmpMasterCode} : ${data.EmpName}`);
        //$('#slInput').text(`${data.TrrCo} : ${data.TrrName}`);
        $('#areaInput').text(`${data.TrrCo} : ${data.TrrName}`);
        $('#zoneInput').text(`${data.AreaCo} : ${data.AreaName}`);
        $('#territoryInput').text(`${data.STrrCo} : ${data.STrrName}`);
        loadSubTerritory(data.ffId, data.RoleType );
    }

    // ---------- Dropdown fillers (keep both classes to match your original) ----------
    function populateTourTypeDropdowns() {
        $('.tour-type-select, .tour-type-select2').each(function () {
            const $s = $(this);
            const prev = $s.val(); // আগের মান থাকলে ধরে রাখার চেষ্টা

            $s.empty();
            $s.append($('<option>', { value: '', text: 'Select Tour Type' })); // placeholder

            $.each(tourTypes || [], function (idx, t) {
                $s.append($('<option>', {
                    value: String(t.TourTypeId),
                    text: t.TourTypeName
                }));
            });

            if (prev && $s.find('option[value="' + prev + '"]').length) {
                $s.val(prev);
            } else {
                $s.val(''); // placeholder-ই selected থাকবে
            }
        });
    }

    function populateSubTerritoryDropdowns() {
        $('.Territory-select, .Territory-select2').each(function () {
            const $s = $(this);
            const prev = $s.val();

            $s.empty();
            $s.append($('<option>', { value: '', text: 'Select Territory' })); // placeholder

            $.each(SubTerritorys || [], function (idx, t) {
                $s.append($('<option>', {
                    value: String(t.SubTerritoryId),
                    text: t.SubTerritoryName
                }));
            });

            if (prev && $s.find('option[value="' + prev + '"]').length) {
                $s.val(prev);
            } else {
                $s.val(''); // placeholder-ই selected থাকবে
            }
        });
    }

    function approvalBadge(status) {
         
        const s = String(status);
        switch (s) {
            case '0': return '<span class="badge bg-secondary">Pending</span>';
            case '1': return '<span class="badge bg-info">Verified</span>';
            case '2': return '<span class="badge bg-success">Approved</span>';
            case '3': return '<span class="badge bg-danger">DisApproved</span>';
            default: return '<span class="badge bg-light text-dark">Draft</span>';
        }
    }
    // ---------- Calendar (kept your IDs per-row to match your earlier selectors) ----------
    function generateVerticalCalendar(month, year) {
        const monthNames = ['January', 'February', 'March', 'April', 'May', 'June',
            'July', 'August', 'September', 'October', 'November', 'December'];
        $('#month-year').text(`${monthNames[month]} ${year}`);

        const vc = document.getElementById('vertical-calendar');
        vc.innerHTML = '';

        const daysInMonth = new Date(year, month + 1, 0).getDate();
        const today = new Date();
        const todayStart = new Date(today.getFullYear(), today.getMonth(), today.getDate()); // 00:00 today
        const dayNames = ['Sunday', 'Monday', 'Tuesday', 'Wednesday', 'Thursday', 'Friday', 'Saturday'];

        for (let day = 1; day <= daysInMonth; day++) {
            const date = new Date(year, month, day);
            const dayOfWeek = date.getDay();
            const dateStr = year + '-' + (month + 1).toString().padStart(2, '0') + '-' + day.toString().padStart(2, '0');
            const isPast = date < todayStart;

            const calendarItem = document.createElement('div');
            calendarItem.classList.add('calendar-item');
            if (day === today.getDate() && month === today.getMonth() && year === today.getFullYear()) {
                calendarItem.classList.add('current-day');
            }

            // full date as data-attr + hidden
            calendarItem.setAttribute('data-date', dateStr);
            const hidden = document.createElement('input');
            hidden.type = 'hidden';
            hidden.className = 'full-date';
            hidden.value = dateStr;
            calendarItem.appendChild(hidden);

            const dateInfo = document.createElement('div');
            dateInfo.classList.add('date-info');
            dateInfo.innerHTML = `
            <div class="date-number">${day}</div>
            <div class="day-info">
                <div class="day-name">${dayNames[dayOfWeek]}</div>
            </div>
        `;

            const dateTable = document.createElement('div');
            dateTable.classList.add('date-table');
            // ⚠️ এখানে কোনো inline <option> নেই; select গুলো খালি রাখা হয়েছে।
            // id ব্যবহার করা হয়নি (duplicate এড়াতে); শুধু name + class রাখা হয়েছে।
            dateTable.innerHTML = `
            <div style="padding:5px;">
              <div class="table-responsive">
                <table class="table table-bordered mb-0">
                  <thead>
                    <tr>
                      <th class="text-center" style="background-color:#FCE4D6">Morning</th>
                      <th class="text-center" style="background-color:#BDD7EE">Evening</th>
                    </tr>
                  </thead>
                  <tbody>
                    <tr>
                      <td>
                        <div class="form-group mb-2">
                          <label class="font-weight-bold">Tour Type:</label>
                          <select class="form-control form-control-sm tour-type-select" name="tourTypeMorning"></select>
                        </div>
                        <div class="form-group mb-2">
                          <label class="font-weight-bold">Territory:</label>
                          <select class="form-control form-control-sm Territory-select" name="territoryMorning"></select>
                        </div>
                        <div class="form-group mb-0">
                          <label class="font-weight-bold">Market:</label>
                          <input type="text" class="form-control form-control-sm" name="marketMorning" placeholder="Market"/>
                        </div>
                      </td>

                      <td>
                        <div class="form-group mb-2">
                          <label class="font-weight-bold">Tour Type:</label>
                          <select class="form-control form-control-sm tour-type-select2" name="tourTypeEvening"></select>
                        </div>
                        <div class="form-group mb-2">
                          <label class="font-weight-bold">Territory:</label>
                          <select class="form-control form-control-sm Territory-select2" name="territoryEvening"></select>
                        </div>
                        <div class="form-group mb-0">
                          <label class="font-weight-bold">Market:</label>
                          <input type="text" class="form-control form-control-sm" name="marketEvening" placeholder="Market"/>
                        </div>
                      </td>
                    </tr>
                  </tbody>
                </table>
              </div>
            </div>
        `;

            calendarItem.appendChild(dateInfo);
            calendarItem.appendChild(dateTable);

            // Lock past dates
            if (isPast) {
                calendarItem.classList.add('past-day');
                const controls = calendarItem.querySelectorAll('select, input, textarea, button');
                controls.forEach(el => {
                    el.disabled = true;
                    el.title = 'Locked: past date';
                });
                const badge = document.createElement('div');
                badge.className = 'locked-badge badge bg-danger text-white';
                badge.textContent = 'Locked (Past)';
                badge.style.cssText = `
               position:absolute; top:8px; right:8px;
               font-size:12px; padding:2px 8px; border-radius:12px;
            `;
                calendarItem.style.position = 'relative';
                calendarItem.appendChild(badge);
            }

            vc.appendChild(calendarItem);
        }

        // after rebuild, fill dropdowns (if arrays already loaded)
        populateTourTypeDropdowns();
        populateSubTerritoryDropdowns();

        // তারপর মাসিক ডেটা বসান
        setTimeout(function () {
            loadTourPlanForMonth(month, year, empId_MAin || 0);
        }, 7000);
    }

    function loadTourPlanForMonth(month, year, empId) {


        const $btns = $('#confirmFinalSubmitBtn, #submitBtn')
            .prop('disabled', false);

        // Disabled লেখা মুছে দিন
        $btns.find('.disabled-text').remove();

        // Tooltip parent থেকে dispose + attributes মুছে দিন
        const $holder = $btns.parent();

        // Bootstrap 5 হলে
        if (typeof bootstrap !== 'undefined' && bootstrap.Tooltip) {
            $holder.each(function () {
                const tip = bootstrap.Tooltip.getInstance(this);
                if (tip) tip.dispose();
            });
        }
        // jQuery tooltip (Bootstrap 4 compatible) হলে
        else if (typeof $holder.tooltip === 'function') {
            $holder.tooltip('dispose');
        }

        // Tooltip সম্পর্কিত সব attributes সরিয়ে ফেলুন
        $holder
            .removeAttr('data-bs-toggle')
            .removeAttr('data-toggle')
            .removeAttr('title')
            .removeAttr('data-original-title') // BS4
            .removeAttr('aria-describedby');

        const m = month + 1; // JS month 0-based, API 1-based
        showLoader('Loading tour plan…', 'Fetching month data');

        $.ajax({
            url: 'TourPlanApps.aspx/GetByMonthTourPlanDataWithData',
            method: 'POST',
            contentType: 'application/json; charset=utf-8',
            dataType: 'json',
            data: JSON.stringify({ empId: empId, month: m, year: year })
        })
            .done(function (res) {
                // WebMethod JSON result comes in res.d
                const rows = res.d || [];
                const byDate = {};

                rows.forEach(r => {
                    const d = (r.TourPlanDate || '').trim();
                    const s = (r.Session || '').trim(); // 'Morning' | 'Evening'
                    if (!d || !s) return;

                    if (!byDate[d]) byDate[d] = { Morning: null, Evening: null };
                    byDate[d][s] = {
                        TourTypeId: r.TourTypeId ?? null,
                        SubTerritoryId: r.SubTerritoryId ?? null,
                        Market: (r.Market ?? '').trim(),
                        ApprovalStatus: (r.ApprovalStatus ?? '').trim() ,
                        ApprovalStatusOthers: (r.ApprovalStatusOthers ?? '').trim()
                    };
                });

                populateCalendarWithData(byDate);
            })
            .fail(function (xhr) {
                console.error('Load failed', xhr);
                if (typeof toastWarn === 'function') toastWarn('Unable to load tour plan data.');
            })
            .always(function () {
                if (typeof hideLoader === 'function') hideLoader();
            });
    }


    function populateCalendarWithData(byDate) {
        let shouldDisable = false;

        $('#vertical-calendar .calendar-item').each(function () {
            const $item = $(this);
            const dateKey = ($item.attr('data-date') || '').trim();
            if (!dateKey || !byDate[dateKey]) return;

            const dayData = byDate[dateKey];

            // Morning
            if (dayData.Morning) {
                setSelectInsideWait($item, 'select[name="tourTypeMorning"]', dayData.Morning.TourTypeId);
                setSelectInsideWait($item, 'select[name="territoryMorning"]', dayData.Morning.SubTerritoryId);
                setInputInside($item, 'input[name="marketMorning"]', dayData.Morning.Market);
                 

                $('#approval-morning').html(approvalBadge(dayData.Morning.ApprovalStatusOthers));

                if (dayData.Morning.ApprovalStatus === '1') {
                    shouldDisable = true;
                }
            }

            // Evening
            if (dayData.Evening) {
                setSelectInsideWait($item, 'select[name="tourTypeEvening"]', dayData.Evening.TourTypeId);
                setSelectInsideWait($item, 'select[name="territoryEvening"]', dayData.Evening.SubTerritoryId);
                setInputInside($item, 'input[name="marketEvening"]', dayData.Evening.Market);
                
                if (dayData.Evening.ApprovalStatus === '1') {
                    shouldDisable = true;
                }
            }
        });

        if (shouldDisable) {
            $('#confirmFinalSubmitBtn, #submitBtn')
                .prop('disabled', true)
                .each(function () {
                    if ($(this).find('.disabled-text').length === 0) {
                        $(this).append(' <span class="disabled-text" style="color:red;">(Disabled)</span>');
                    }
                })
                .parent()
                .attr('data-toggle', 'tooltip')
                .attr('title', 'Can not submit. Already submitted.')
                .tooltip({ trigger: 'hover focus', placement: 'top' });
        } else {
            const $btns = $('#confirmFinalSubmitBtn, #submitBtn')
                .prop('disabled', false);

            // Disabled লেখা মুছে দিন
            $btns.find('.disabled-text').remove();

            // Tooltip parent থেকে dispose + attributes মুছে দিন
            const $holder = $btns.parent();

            if (typeof bootstrap !== 'undefined' && bootstrap.Tooltip) {
                $holder.each(function () {
                    const tip = bootstrap.Tooltip.getInstance(this);
                    if (tip) tip.dispose();
                });
            } else if (typeof $holder.tooltip === 'function') {
                $holder.tooltip('dispose');
            }

            $holder
                .removeAttr('data-bs-toggle')
                .removeAttr('data-toggle')
                .removeAttr('title')
                .removeAttr('data-original-title')
                .removeAttr('aria-describedby');
        }
    }

    function setSelectInsideWait($scope, selector, value, retries = 15, delayMs = 120) {
        if (value === undefined || value === null) return;

        const $sel = $scope.find(selector);
        if (!$sel.length) return;

        const target = String(value);

        const trySet = () => {
            // options এখনো না এলে একটু অপেক্ষা
            if ($sel.find('option').length === 0) {
                if (retries-- > 0) return setTimeout(trySet, delayMs);
                // শেষ চেষ্টা: placeholder রেখে দিলাম
                $sel.val('').trigger('change');
                return;
            }

            // চাইলে স্ট্রিং কম্পেয়ার নিশ্চিত করুন
            if ($sel.find('option[value="' + target + '"]').length) {
                $sel.val(target).trigger('change');
            } else {
                // ভুলে যদি server numeric দেয় আর option string হয় / বা option না মেলে
                // কিছু না বদলে placeholder-ই থাকুক (silent fail better than wrong data)
                $sel.val('').trigger('change');
            }
        };

        trySet();
    }

    function setInputInside($scope, selector, value) {
        if (value === undefined || value === null) return;
        const $inp = $scope.find(selector);
        if ($inp.length) {
            $inp.val(String(value)).trigger('input');
        }
    }



    function submitTourPlan(type, remarks) {
        const submitBtn = (type === 'final') ? $('#confirmFinalSubmitBtn') : $('#submitBtn');
        const originalText = submitBtn.html();

        submitBtn.html('<i class="fas fa-spinner fa-spin"></i> Submitting...');
        submitBtn.prop('disabled', true);

        showLoader('Submitting tour plan…', type === 'final' ? 'Final submission' : 'Saving as draft');

          

        const nvl = (v) => (v === undefined || v === null || String(v).trim() === '' ? null : String(v).trim());

        const rows = [];
        $('#vertical-calendar .calendar-item').each(function () {
            const $row = $(this);

            // expected format: yyyy-MM-dd
            const fullDate = nvl(($row.data('date') || $row.find('.full-date').val() || ''));
            const dayText = ($row.find('.date-number').text() || '').trim();
            const serialNo = dayText ? parseInt(dayText, 10) : null;

            // Use [name=...] to avoid duplicate id collisions
            const tourTypeMorning = nvl($row.find('[name="tourTypeMorning"], #tourTypeMorning').val());
            const territoryMorning = nvl($row.find('[name="territoryMorning"], #territoryMorning').val());
      
            const marketMorning = $row.find('[name="marketMorning"]').val();

            const tourTypeEvening = nvl($row.find('[name="tourTypeEvening"], #tourTypeEvening').val());
            const territoryEvening = nvl($row.find('[name="territoryEvening"], #territoryEvening').val());
            /*const marketEvening = nvl($row.find('[name="marketEvening"], #marketEvening').val());*/
            const marketEvening = $row.find('[name="marketEvening"]').val();

            const hasMorning = (tourTypeMorning || territoryMorning || marketMorning);
            const hasEvening = (tourTypeEvening || territoryEvening || marketEvening);

            if (!fullDate || (!hasMorning && !hasEvening)) return;

            rows.push({
                TourPlanDate: fullDate,   // yyyy-MM-dd
                SerialNo: serialNo,
                EmpId: empId_MAin,

                // Morning set
                MorTourTypeId: tourTypeMorning ? parseInt(tourTypeMorning, 10) : null,
                MorTerritoryId: territoryMorning ? parseInt(territoryMorning, 10) : null,
                MorMarketId: (marketMorning !== undefined && marketMorning !== null && String(marketMorning).trim() !== '')
                    ? String(marketMorning).trim() : null,

                // Evening set
                EveTourTypeId: tourTypeEvening ? parseInt(tourTypeEvening, 10) : null,
                EveTerritoryId: territoryEvening ? parseInt(territoryEvening, 10) : null,
                EveMarketId: (marketEvening !== undefined && marketEvening !== null && String(marketEvening).trim() !== '')
                    ? String(marketEvening).trim() : null
            });
        });

        if (rows.length === 0) {
            hideLoader();
            toastr.warning('Nothing to submit. Please fill at least one Morning/Evening entry.');
            
            submitBtn.html(originalText).prop('disabled', false);
            return;
        }

        const payload = { aInfo: { Type: type, remarks: remarks, aTourPlanInfo: rows } };

        $.ajax({
            type: "POST",
            url: "TourPlanApps.aspx/SubmitTourPlan",
            data: JSON.stringify(payload),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (response) {
                hideLoader();
                let ok = false, msg = 'Saved.';
                if (response && response.d) {
                    if (typeof response.d.isSuccess !== 'undefined') {
                        ok = !!response.d.isSuccess;
                        msg = response.d.Msd || response.d.ErrorMessage || msg;
                    } else if (typeof response.d.success !== 'undefined') {
                        ok = !!response.d.success;
                        msg = response.d.message || msg;
                    }
                }
                if (ok) {
                    $('#successMessage').stop(true, true).fadeIn();
                    setTimeout(function () { $('#successMessage').fadeOut(); }, 5000);
                } else {
                    toastr.error(msg || 'Save failed.');
 
                }
                submitBtn.html(originalText).prop('disabled', false);
                if (type === 'final' && (msg.toLowerCase().includes("success") || msg.toLowerCase().includes("saved"))) {
                    
                    window.location.href = "/TourPlanApps.aspx?EmpId=" + empId_MAin;
                }

            },
            error: function (xhr, status, error) {
                hideLoader();
                const msg = (xhr && xhr.responseJSON && (xhr.responseJSON.Message || xhr.responseJSON.message)) || error || status;
                toastr.error(  'Error submitting tour plan: ' + msg);

                submitBtn.html(originalText).prop('disabled', false);
            }
        });
    }

    // --- Open modal on Final button click
    $('#finalSubmitBtn').on('click', function () {
        const rows = collectTourPlanRows(); // তোমার আগের collector
        if (!rows.length) { alert('Nothing to submit.'); return; }

        const counts = computeHqEqOsTotalsBySession(rows);

        // Overall totals (আগের ৩টা)
        $('#totHQ').text(counts.Total.HQ);
        $('#totEQ').text(counts.Total.EQ);
        $('#totOS').text(counts.Total.OS);

        // Morning vs Evening breakdown
        $('#totM_HQ').text(counts.Morning.HQ);
        $('#totM_EQ').text(counts.Morning.EQ);
        $('#totM_OS').text(counts.Morning.OS);
        $('#totM_Total').text(counts.Morning.Total);

        $('#totE_HQ').text(counts.Evening.HQ);
        $('#totE_EQ').text(counts.Evening.EQ);
        $('#totE_OS').text(counts.Evening.OS);
        $('#totE_Total').text(counts.Evening.Total);

        openFinalModal(); // BS4/5-safe helper (আগে দেওয়া)
    });


    function collectTourPlanRows() {
        const nvl = (v) => (v === undefined || v === null || String(v).trim() === '' ? null : String(v).trim());
        const rows = [];

        $('#vertical-calendar .calendar-item').each(function () {
            const $row = $(this);

            const fullDate = nvl(($row.data('date') || $row.find('.full-date').val() || '')); // yyyy-MM-dd
            const dayText = ($row.find('.date-number').text() || '').trim();
            const serialNo = dayText ? parseInt(dayText, 10) : null;

            const tourTypeMorning = nvl($row.find('[name="tourTypeMorning"], #tourTypeMorning').val());
            const territoryMorning = nvl($row.find('[name="territoryMorning"], #territoryMorning').val());
            const marketMorning = $row.find('[name="marketMorning"]').val();

            const tourTypeEvening = nvl($row.find('[name="tourTypeEvening"], #tourTypeEvening').val());
            const territoryEvening = nvl($row.find('[name="territoryEvening"], #territoryEvening').val());
            const marketEvening = $row.find('[name="marketEvening"]').val();

            const hasMorning = (tourTypeMorning || territoryMorning || marketMorning);
            const hasEvening = (tourTypeEvening || territoryEvening || marketEvening);

            if (!fullDate || (!hasMorning && !hasEvening)) return;

            rows.push({
                TourPlanDate: fullDate,
                SerialNo: serialNo,
                EmpId: empId_MAin,

                MorTourTypeId: tourTypeMorning ? parseInt(tourTypeMorning, 10) : null,
                MorTerritoryId: territoryMorning ? parseInt(territoryMorning, 10) : null,
                MorMarketId: (marketMorning !== undefined && marketMorning !== null && String(marketMorning).trim() !== '')
                    ? String(marketMorning).trim() : null,

                EveTourTypeId: tourTypeEvening ? parseInt(tourTypeEvening, 10) : null,
                EveTerritoryId: territoryEvening ? parseInt(territoryEvening, 10) : null,
                EveMarketId: (marketEvening !== undefined && marketEvening !== null && String(marketEvening).trim() !== '')
                    ? String(marketEvening).trim() : null
            });
        });

        return rows;
    }

    // --- Compute totals HQ/EQ/OS from rows
    function computeHqEqOsTotals(rows) {
        const map = buildTourTypeCodeMap();
        let HQ = 0, EQ = 0, OS = 0;

        rows.forEach(r => {
            if (r.MorTourTypeId && map[r.MorTourTypeId]) {
                if (map[r.MorTourTypeId] === 'HQ') HQ++;
                else if (map[r.MorTourTypeId] === 'EQ') EQ++;
                else if (map[r.MorTourTypeId] === 'OS') OS++;
            }
            if (r.EveTourTypeId && map[r.EveTourTypeId]) {
                if (map[r.EveTourTypeId] === 'HQ') HQ++;
                else if (map[r.EveTourTypeId] === 'EQ') EQ++;
                else if (map[r.EveTourTypeId] === 'OS') OS++;
            }
        });

        return { HQ, EQ, OS };
    }
    // rows থেকে Morning/Evening ব্রেকডাউন বের করুন
    function computeHqEqOsTotalsBySession(rows) {
        const map = buildTourTypeCodeMap();
        const res = {
            Morning: { HQ: 0, EQ: 0, OS: 0, Total: 0 },
            Evening: { HQ: 0, EQ: 0, OS: 0, Total: 0 },
            Total: { HQ: 0, EQ: 0, OS: 0, Total: 0 }
        };

        rows.forEach(r => {
            const hasM = !!(r.MorTourTypeId || r.MorTerritoryId || r.MorMarketId);
            const hasE = !!(r.EveTourTypeId || r.EveTerritoryId || r.EveMarketId);

            if (hasM) { res.Morning.Total++; res.Total.Total++; }
            if (hasE) { res.Evening.Total++; res.Total.Total++; }

            const codeM = r.MorTourTypeId ? map[r.MorTourTypeId] : undefined;
            if (codeM) { res.Morning[codeM]++; res.Total[codeM]++; }

            const codeE = r.EveTourTypeId ? map[r.EveTourTypeId] : undefined;
            if (codeE) { res.Evening[codeE]++; res.Total[codeE]++; }
        });

        return res;
    }
    function buildTourTypeCodeMap() {
        const map = {};
        (tourTypes || []).forEach(t => {
            const id = t.TourTypeId;
            const name = (t.TourTypeName || '').toUpperCase();
            if (!id) return;
            if (name.includes('OS') || name.includes('OUTSTATION')) map[id] = 'OS';
            else if (name.includes('EQ') || name.includes('EX-HQ') || name.includes('EXHQ')) map[id] = 'EQ';
            else if (name.includes('HQ')) map[id] = 'HQ';
        });
        return map;
    }
    // --- Confirm Final Submit => call existing submitTourPlan('final', remarks)
    $('#confirmFinalSubmitBtn').on('click', function () {
        const remarks = ($('#finalRemarks').val() || '').trim();

        if (!remarks) {
                 toastr.warning("Remarks is required before final submission.");
            
            return; // 🚫 সাবমিট বন্ধ হয়ে যাবে
        }
        closeFinalModal();
        submitTourPlan('final', remarks);
    });
</script>
</body>
</html>
