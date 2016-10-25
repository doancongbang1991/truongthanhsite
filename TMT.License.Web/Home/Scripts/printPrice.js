function printPrice()  
{  
    var dataUrl = document.getElementById('canvasGraphic').toDataURL(); //attempt to save base64 string to server using this var  
	var statPriceTable = '<table id="statPriceTable" class="abcde statDataTable">' + $('#statPriceTable').html() + '</table>';
	var statPriceTableSum = '<table id="statPriceTableSum" class="statDataTable statLast">' + $('#statPriceTableSum').html() + '</table>';
	
	var Province_Code = $('#autocalc').find('input[name=Province_Code]:checked').val();
	var strLocation = "";
	if(Province_Code == "HCM") {
		strLocation = "Hồ Chí Minh";
	}
	else if(Province_Code == "DNG") {
		strLocation = "Đà Nẵng";
	}
	else if(Province_Code == "HNO") {
		strLocation = "Hà Nội";
	}
	else {
		strLocation = "Khác";
	}
	
	var Type = $('#autocalc').find('input[name=Type]:checked').val();
	var strType = "";
	if(Type == 'nhapho') {
		strType = "Nhà phố, căn hộ";
	}
	else if(Type == 'bietthu') {
		strType = "Biệt thự, nhà từ 2 mặt tiền trở lên";
	}
	else if(Type == 'serviceA') {
		strType = "Bar, Cafe, Nhà hàng, Showroom, Shop";
	}
	else if(Type == 'serviceB') {
		strType = "Khách sạn, Resort, Karaoke";
	}
	
	var WeakGround = $('#autocalc').find('input[name=WeakGround]:checked').val();
	var strTTKhac = "";
	if(WeakGround) {
		strTTKhac = "Nền đất yếu";
	}
	
	var SmallAlley = $('#autocalc').find('input[name=SmallAlley]:checked').val();
	if(SmallAlley) {
		if(strTTKhac != "") {
			strTTKhac += ", nhà trong hẻm nhỏ hơn 4m (cấm xe tải)";
		}
		else {
			strTTKhac += "Nhà trong hẻm nhỏ hơn 4m (cấm xe tải)";
		}
	}
	
	var LandSize = parseFloat($('#autocalc').find('input[name=LandSize]').val());
	var ConstructionSize = parseFloat($('#autocalc').find('input[name=ConstructionSize]').val());
	var GardenSize = LandSize - ConstructionSize;
	var Floor = parseFloat($('#autocalc').find('input[name=Floor]').val());
	
    var windowContent = '<!DOCTYPE html>';
    windowContent += '<html>'
    windowContent += '<head><title>Print - Khái toán xây dựng</title>';
	windowContent += '<link href="/Content/css/vm.reset.css" rel="stylesheet" type="text/css" />';
	windowContent += '<link href="/Content/Site.css" rel="stylesheet" type="text/css" />';
	windowContent += '<link href="/Content/css/visualize.jQuery.css" rel="stylesheet" type="text/css" />';
	windowContent += '<link href="/Content/css/printPrice.css" rel="stylesheet" type="text/css" />';
	windowContent += '</head>';
    windowContent += '<body>'
	windowContent += '<div id="headerPrint">';
	windowContent += '<div id="printLogo"><img src="/Content/images/logo.png" width="186" height="122" /></div>';
	windowContent += '<div class="clear"></div>';
	windowContent += '</div>';
	windowContent += '<div class="clear"></div>';
	windowContent += '<h1>KHÁI TOÁN MỨC ĐẦU TƯ</h1>';
	windowContent += '<div class="clear"></div>';
	windowContent += '<ul id="selectedInfo">';
	windowContent += '<li>Địa điểm: ' + strLocation + '<sup>&nbsp;</sup></li>';
	windowContent += '<li>Loại công trình: ' + strType + '<sup>&nbsp;</sup></li>';
	windowContent += '<li>Diện tích trong sổ đỏ: ' + String(LandSize) + 'm<sup>2</sup></li>';
	windowContent += '<li>Diệt tích xây dựng tầng trệt: ' + String(ConstructionSize) + 'm<sup>2</sup></li>';
	windowContent += '<li>Diệt tích sân hoặc vườn: ' + String(GardenSize) + 'm<sup>2</sup></li>';
	windowContent += '<li>Số tầng: ' + String(Floor) + '<sup>&nbsp;</sup></li>';
	if(strTTKhac != "") {
	windowContent += '<li>Hiện trạng: ' + strTTKhac + '<sup>&nbsp;</sup></li>';
	}
	windowContent += '</ul>'; //selectedInfo
	windowContent += '<div class="clear"></div>';	
	windowContent += '<div id="priceTableAndPriceGraphic">';
	windowContent += '<div id="priceTable">' + statPriceTable + statPriceTableSum + '</div>';
	windowContent += '<div id="priceGraphic"><img src="' + dataUrl + '"></div>';
	windowContent += '</div>'; //priceTableAndPriceGraphic
	windowContent += '<div class="clear"></div>';
	windowContent += '<div id="moreInfo">';
	windowContent += '<p>Đơn giá trên chưa bao gồm thuế VAT</p>';
	windowContent += '<p>Chưa bao gồm những hạng mục khác như: Thang máy, hồ bơi, sân vườn</p>';
	windowContent += '</div>';
	windowContent += '</div>'; //moreInfo
    windowContent += '</body>';
    windowContent += '</html>';
	
	var f = new Iframe();
	var writeDoc;
	var printWindow;
			
	writeDoc = f.doc;
    printWindow = f.contentWindow || f;
	
	writeDoc.open();
	writeDoc.write( windowContent );
	writeDoc.close();
	
	printWindow.focus();
	printWindow.print();	
}

var Iframe = function ()
{
	var frameId = "printPriceFrame";
	var iframeStyle = 'border:0;position:absolute;width:0px;height:0px;right:0px;top:0px;';
	var iframe;

	try
	{
		iframe = document.createElement('iframe');
		document.body.appendChild(iframe);
		$(iframe).attr({ style: iframeStyle, id: frameId, src: "" });
		iframe.doc = null;
		iframe.doc = iframe.contentDocument ? iframe.contentDocument : ( iframe.contentWindow ? iframe.contentWindow.document : iframe.document);
	}
	catch( e ) { throw e + ". iframes may not be supported in this browser."; }

	if ( iframe.doc == null ) throw "Cannot find document.";

	return iframe;
}