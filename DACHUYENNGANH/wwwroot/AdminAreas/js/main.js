var tienVay = document.getElementById('money');
var time = document.getElementById('time');
var giaingan = document.getElementById('giaingan');
var lai = document.getElementById('lai');

var body = document.getElementById('body');

function final() {
    var tienGoc = tienVay.value.toLocaleString();
    var thoiGianVay = time.value;
    var ngayGiaiNgan = new Date(giaingan.value);
    var laiSuat = lai.value;

   
    result(tienGoc, laiSuat, thoiGianVay, ngayGiaiNgan)
}

//Tính tiền gốc hàng tháng
function calculateTienGoc(tienGoc, time) {
    let tienGocHangThang = tienGoc/time;
    return tienGocHangThang;
}

// Tính tiền lãi hàng tháng
function calculateTienLai(gocConLai, lai) {
    let tienLaiHangThang = gocConLai*(lai/100)/12;
    return tienLaiHangThang;
}

// Tính toán
function result(tienGoc, laiSuat, time, ngayGiaiNgan) {
    var tienGocConLai = tienGoc;
    var gocHangThang = calculateTienGoc(tienGoc, time);
    var tongTienPhaiTra = 0;
    var tongTienLai = 0;
    taoBang(ngayGiaiNgan, 0, tienGocConLai)
    for(let i = 1; i <= time; i++){
        var laiHangThang = calculateTienLai(tienGocConLai, laiSuat)
        var tienPhaiTraHangThang = laiHangThang + gocHangThang;
        tienGocConLai = tienGocConLai - gocHangThang;

        
        ngayGiaiNgan.setMonth(ngayGiaiNgan.getMonth() + 1)
        updateDate(ngayGiaiNgan)
        taoBang(ngayGiaiNgan, i, tienGocConLai, gocHangThang, laiHangThang, tienPhaiTraHangThang )
        tongTienPhaiTra += gocHangThang
        tongTienLai += laiHangThang

    }

    taoHangTongKq(tongTienPhaiTra, tongTienLai, tongTienPhaiTra + tongTienLai)
}

// hiển thị kết quả
function taoBang(ngayGiaiNgan, index, gocConLai, gocHangThang, laiHangThang, tienPhaiTraHangThang) {
    var hang = body.insertRow(-1);
    var cot1 = hang.insertCell(0);
    var cot2 = hang.insertCell(1);
    var cot3 = hang.insertCell(2);
    var cot4 = hang.insertCell(3);
    var cot5 = hang.insertCell(4);
    var cot6 = hang.insertCell(5);

    if(index == 0) {
        cot1.innerText = updateDate(ngayGiaiNgan);
        cot2.innerText = index;
        cot3.innerText = updateNumber(gocConLai);
        cot4.innerText = '';
        cot5.innerText = '';
        cot6.innerText = '';
    } else {
        cot1.innerText = ngayGiaiNgan;
        cot2.innerText = index;
        cot3.innerText = updateNumber(gocConLai);
        cot4.innerText = updateNumber(gocHangThang);
        cot5.innerText = updateNumber(laiHangThang);
        cot6.innerText = updateNumber(tienPhaiTraHangThang);
    }
}

// Tạo hàng cuối tính tổng
function taoHangTongKq(tongGoc, tongLai, tongTienPhaiTra) {
    var hang = body.insertRow(-1);
    var cot1 = hang.insertCell(0);
    var cot2 = hang.insertCell(1);
    var cot3 = hang.insertCell(2);
    var cot4 = hang.insertCell(3);
    var cot5 = hang.insertCell(4);
    var cot6 = hang.insertCell(5);

    cot1.innerHTML = `<b> Tổng </b>`;
    cot2.innerHTML = '';
    cot3.innerHTML = '';
    cot4.innerHTML = `<b> ${updateNumber(tongGoc)} </b>`;
    cot5.innerHTML = `<b> ${updateNumber(tongLai)} </b>`;
    cot6.innerHTML = `<b> ${updateNumber(tongTienPhaiTra)} </b>`;
}

// Update lại ngày
function updateDate(date) {
    
    return date.getDate()+'-'+(date.getMonth()+1)+'-'+date.getFullYear();
}

// Update lại số
function updateNumber(number) {
   return Math.round(number).toLocaleString();
}

