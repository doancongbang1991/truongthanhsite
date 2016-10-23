/***************************************************************
 * Coder: Phan Nguyên Như (nhuphan@vietmeta.com, +84986030757) *
 ***************************************************************/
// pre-submit callback 
function showRequest(formData, jqForm, options) {
    // formData is an array; here we use $.param to convert it to a string to display it 
    // but the form plugin does this for you automatically when it submits the data 
//    var queryString = $.param(formData);

    // jqForm is a jQuery object encapsulating the form element.  To access the 
    // DOM element for the form do this: 
    // var formElement = jqForm[0]; 

//    alert('About to submit: \n\n' + queryString);

    // here we could return false to prevent the form from being submitted; 
    // returning anything other than false will allow the form submit to continue 
    var loadingImg = '<img src="/Content/images/s16/loading.gif" />';
    $('.price1').html(loadingImg);
    $('.price2').html(loadingImg);
    $('.price3').html(loadingImg);
	$('.price4').html(loadingImg);
    return true;
}

// post-submit callback 
function showResponse(responseText, statusText, xhr, $form) {
    // for normal html responses, the first argument to the success callback 
    // is the XMLHttpRequest object's responseText property 

    // if the ajaxForm method was passed an Options Object with the dataType 
    // property set to 'xml' then the first argument to the success callback 
    // is the XMLHttpRequest object's responseXML property 

    // if the ajaxForm method was passed an Options Object with the dataType 
    // property set to 'json' then the first argument to the success callback 
    // is the json data object returned by the server

//    alert('status: ' + statusText + '\n\nresponseText: \n' + responseText +
//        '\n\nThe output div should have already been updated with the responseText.');
    var priceTong = responseText.ConstructPrice + responseText.FinishPrice;
	var noithatPrice = responseText.FinishPrice * 0.51;
	if(responseText.FinishPackage == 'kha') {
		noithatPrice = responseText.FinishPrice * 0.61;
	}
	else if(responseText.FinishPackage == 'caocap') {
		noithatPrice = responseText.FinishPrice * 0.71;
	}
	else if(responseText.FinishPackage == 'sangtrong') {
		noithatPrice = responseText.FinishPrice * 0.91;
	}
	
	priceTong += responseText.DesignPrice;
	var phatsinhPrice = priceTong * 0.1;
	//objPreInput.FinishPackage = $('#autocalc').find('input[name=FinishPackage]:checked').val();
	//responseText.FinishPackage
	
	$('.price1').html('<span style="color:red;">' + formatMoney(responseText.DesignPrice) + '</span> VND');
    $('.price2').html('<span style="color:red;">' + formatMoney(responseText.ConstructPrice) + '</span> VND');
    $('.price3').html('<span style="color:red;">' + formatMoney(responseText.FinishPrice) + '</span> VND');
	$('.price4').html('<span style="color:red;">' + formatMoney(noithatPrice) + '</span> VND');
	$('.price5').html('<span style="color:red;">' + formatMoney(phatsinhPrice) + '</span> VND');
	$('.priceTong').html('<span style="color:red;">' + formatMoney(priceTong + noithatPrice + phatsinhPrice) +'</span> VND');
	
	if(responseText.DesignPrice > 0 && responseText.ConstructPrice > 0 && responseText.FinishPrice > 0 && noithatPrice > 0) {
		$('.visualize').remove();
		$('table.abcde').visualize({type: 'pie', height: 324, pieMargin: 0, title: ''});	
	}
	if(responseText.DesignPrice == 0) {
		$('.DesignPriceNote').show(); 
	}
	else {
		$('.DesignPriceNote').hide(); 
	}
	if(responseText.ConstructPrice == 0) {
		$('.ConstructPriceNote').show(); 
	}
	else {
		$('.ConstructPriceNote').hide(); 
	}	
	
    //alert(responseText.Fullname);
    
    $('.p-trongoi').find('.g').html(formatMoney(responseText.DesignPriceTrongoiRef));
    $('.p-kientruc').find('.g').html(formatMoney(responseText.DesignPriceKientrucRef));
    $('.p-noithat').find('.g').html(formatMoney(responseText.DesignPriceNoithatRef));
	
	$('.p-caitaotrongoi').find('.g').html(formatMoney(responseText.DesignPriceCaitaoTrongoiRef));
    $('.p-caitaokientruc').find('.g').html(formatMoney(responseText.DesignPriceCaitaoKientrucRef));
    $('.p-caitaonoithat').find('.g').html(formatMoney(responseText.DesignPriceCaitaoNoithatRef));
	
	$('.p-noithatnhanh').find('.g').html(formatMoney(responseText.DesignPriceNoithatNhanhRef));
    $('.p-caitaonhanh').find('.g').html(formatMoney(responseText.DesignPriceCaitaoNhanhRef));
    
    $('.p-construct-1').html(responseText.ConstructPriceRef1);
    $('.p-construct-2').html(responseText.ConstructPriceRef2);
	
	if(responseText.QuanTrungTamServer) {
		//$('input[name=QuanTrungTam]').attr('checked', 'checked');
		$('input[name=QuanTrungTam], label[for=QuanTrungTam1]').show();
	}
	else {
		$('input[name=QuanTrungTam]').removeAttr('checked');
		$('input[name=QuanTrungTam], label[for=QuanTrungTam1]').hide();
	}

    if ((responseText.DesignLinkPDF != '') && !isNaN(responseText.DesignLinkPDF)) {
        $('#contract_auto').show();

        $('#contract_auto .design_link_pdf').html(responseText.DesignLinkPDF);
        $('#contract_auto .design_link_docx').html(responseText.DesignLinkDOCX);

        $('#contract_auto .construct_link_pdf').html(responseText.ConstructLinkPDF);
        $('#contract_auto .construct_link_docx').html(responseText.ConstructLinkDOCX);
    }
	
	if(responseText.XPXDimage.indexOf("xp0") > -1) {
		$('#koDuTieuChuan').slideDown(400, function() {
			$('#koDuTieuChuan').show();
		});
	}
	else {
		$('#koDuTieuChuan').slideUp(400, function() {
			$('#koDuTieuChuan').hide();
		});
	}

	if((responseText.XPXDimage.indexOf("xp0") > -1) || (responseText.DTngoaiLG >= 150)) {
		$('input[name=ChieurongMT], label[for=ChieurongMT1]').show();
	}
	else {
		$('input[name=ChieurongMT]').val('');
		$('input[name=ChieurongMT], label[for=ChieurongMT1]').hide();
	}
	
	$("#XPXDimage").html('<p class="loading"><img src="/Content/images/loading.gif" /></p>');
	var iXpxd = new Image();
	iXpxd.src = '/Content/images/xpxd/' + responseText.XPXDimage + '.jpg';
	$('#XPXDimage').html('<img src="/Content/images/xpxd/' + responseText.XPXDimage + '.jpg" alt="" />');
	iXpxd.onload = function () {
		$('#XPXDimage').html('<img src="' + this.src + '" alt="" />');
	}
	
	if((responseText.MDXD > 0) && (responseText.DTngoaiLG > 0)) {
		$('#MDXD').html('<p>Mật độ xây dựng tối đa: <strong>' + responseText.MDXD + '%</strong></p>').show();
	}
	if(responseText.Logioi > 0) {
		$('#VuonBanCong').html('<p>Độ vươn ban công tối đa: <strong>' + responseText.VuonBanCong + 'm</strong></p>').show();
	}
}

function preDefine() {
    this.giaThietke = _giaThietke; //Lay tu file js khac /File/ScriptPrice
    this.giaPhantho = _giaPhantho;
    this.giaHoanthien = _giaHoanthien;
}

function preInput() {
    this.objPreDefine = new preDefine();
}

$(document).ready(function () {
	var sum1 = 0; 
	var sum2 = 0;
	$('#CN_m_i').click(function() {
		$('.cn').show();
		$('.dn').hide();
		$('.cndn').slideDown();
	});
	$('#DN_m_i').click(function() {
		$('.dn').show();
		$('.cn').hide();
		$('.cndn').slideDown();
		$('#Birth_m_i').val('0');
	});
	
	$('#printButton').click(function() {
		printPrice();
		return false;
	});
	
	$('#goi-tiet-kiem .list1 .product-item .price-calc').each(function(index) {
		sum1 += parseFloat($(this).text().replace(/,/g, ''));
	});
	$('#goi-tiet-kiem .sum1').html(formatMoney(sum1));
	
	$('#goi-tiet-kiem .list2 .product-item .price-calc').each(function(index) {
		sum2 += parseFloat($(this).text().replace(/,/g, ''));
	});
	$('#goi-tiet-kiem .sum2').html(formatMoney(sum2));
	
	sum1 = 0;sum2 = 0;
	$('#goi-trung-binh .list1 .product-item .price-calc').each(function(index) {
		sum1 += parseFloat($(this).text().replace(/,/g, ''));
	});
	$('#goi-trung-binh .sum1').html(formatMoney(sum1));
	
	$('#goi-trung-binh .list2 .product-item .price-calc').each(function(index) {
		sum2 += parseFloat($(this).text().replace(/,/g, ''));
	});
	$('#goi-trung-binh .sum2').html(formatMoney(sum2));	
	
	sum1 = 0;sum2 = 0;
	$('#goi-kha .list1 .product-item .price-calc').each(function(index) {
		sum1 += parseFloat($(this).text().replace(/,/g, ''));
	});
	$('#goi-kha .sum1').html(formatMoney(sum1));
	
	$('#goi-kha .list2 .product-item .price-calc').each(function(index) {
		sum2 += parseFloat($(this).text().replace(/,/g, ''));
	});
	$('#goi-kha .sum2').html(formatMoney(sum2));
	
	sum1 = 0;sum2 = 0;
	$('#goi-cao-cap .list1 .product-item .price-calc').each(function(index) {
		sum1 += parseFloat($(this).text().replace(/,/g, ''));
	});
	$('#goi-cao-cap .sum1').html(formatMoney(sum1));
	
	$('#goi-cao-cap .list2 .product-item .price-calc').each(function(index) {
		sum2 += parseFloat($(this).text().replace(/,/g, ''));
	});
	$('#goi-cao-cap .sum2').html(formatMoney(sum2));
	
	sum1 = 0;sum2 = 0;
	$('#goi-sang-trong .list1 .product-item .price-calc').each(function(index) {
		sum1 += parseFloat($(this).text().replace(/,/g, ''));
	});
	$('#goi-sang-trong .sum1').html(formatMoney(sum1));
	
	$('#goi-sang-trong .list2 .product-item .price-calc').each(function(index) {
		sum2 += parseFloat($(this).text().replace(/,/g, ''));
	});
	$('#goi-sang-trong .sum2').html(formatMoney(sum2));	
	
    $('#like_address').click(function () {
        $('#Address2_m_i').val($('#Address_m_i').val());
        $('#Province_Code2_m_i').val($('#Province_Code1_m_i').val());
        return false;
    });
    $('#createcontract').click(function () {
        calcPrice();
        var loadingImg = '<img src="/Content/images/s16/loading.gif" />';
        $('#contract_auto').slideUp();
        $('#contract_auto_loading').html(loadingImg);
        $('#contract_auto_loading').slideDown();

        $.getJSON("/File/CreateContract", function (responseText) {
			//MISSING_INFO
			//DELAY_1_MINUTE
			//DCOM_ERROR
			//ORGINAL_FILE_MISSING
			if (responseText.DesignLinkPDF == 'MISSING_INFO') {
				$('#contract_auto_loading').html('<span class="error">Vui lòng cung cấp đầy đủ thông tin để làm hợp đồng!</span>');
			}
			else if (responseText.DesignLinkPDF == 'DELAY_1_MINUTE') {
				$('#contract_auto_loading').html('<span class="error">Bạn vừa bấm tạo hợp đồng, vui lòng đợi 1 phút trước khi bấm lại lần nữa!</span>');
			}
			else if (responseText.DesignLinkPDF == 'DCOM_ERROR') {
				$('#contract_auto_loading').html('<span class="error">Hệ thống đang được bảo trì, vui lòng quay lại sau!</span>');
			}
			else if (responseText.DesignLinkPDF == 'ORGINAL_FILE_MISSING') {
				$('#contract_auto_loading').html('<span class="error">File mẫu đang tạm ngưng để thay đổi nội dung, vui lòng quay lại sau!!</span>');
			}
			else if (responseText.DesignLinkPDF == 'PDFXPS_FAILED') {
				$('#contract_auto_loading').slideUp();
                $('#contract_auto').slideDown();
                $('a.design_link_docx').attr('href', 'File/Download?f=contract,' + responseText.DesignLinkDOCX.replace(/\//gi, ","));
				$('a.design_link_pdf').hide();
				$('a.design_link_xps').hide();
				
				$('a.construct_link_docx').attr('href', 'File/Download?f=contract,' + responseText.ConstructLinkDOCX.replace(/\//gi, ","));
				$('a.construct_link_pdf').hide();
				$('a.construct_link_xps').hide();
			}
			else {
                $('#contract_auto_loading').slideUp();
                $('#contract_auto').slideDown();
                $('a.design_link_docx').attr('href', 'File/Download?f=contract,' + responseText.DesignLinkDOCX.replace(/\//gi, ","));
				$('a.design_link_pdf').attr('href', 'File/Download?f=contract,' + responseText.DesignLinkPDF.replace(/\//gi, ","));
				$('a.design_link_xps').attr('href', 'File/Download?f=contract,' + responseText.DesignLinkXPS.replace(/\//gi, ","));
                
				$('a.construct_link_docx').attr('href', 'File/Download?f=contract,' + responseText.ConstructLinkDOCX.replace(/\//gi, ","));
                $('a.construct_link_pdf').attr('href', 'File/Download?f=contract,' + responseText.ConstructLinkPDF.replace(/\//gi, ","));
				$('a.construct_link_xps').attr('href', 'File/Download?f=contract,' + responseText.ConstructLinkXPS.replace(/\//gi, ","));
            }
        });

        return false;
    });
    $('#autocalc').submit(function () {
		var options = {
            //target: '#output1',   // target element(s) to be updated with server response 
            beforeSubmit: showRequest,  // pre-submit callback 
            success: showResponse,  // post-submit callback 

            // other available options: 
            //url:       url         // override for form's 'action' attribute 
            //type:      type        // 'get' or 'post', override for form's 'method' attribute 
            dataType: 'json'        // 'xml', 'script', or 'json' (expected server response type) 
            //clearForm: true        // clear all form fields after successful submit 
            //resetForm: true        // reset the form after successful submit 

            // $.ajax options can be used here too, for example: 
            //timeout:   3000 
        };
        $(this).ajaxSubmit(options);
        return false;
    });

    var objPreInput = new preInput();

    var changePreInput = function () {
		$('input[name=DesignPackage]').parent().css('background','');
		$('.pb').css({'border-right':'1px solid #ccc'});
		$('input[name=DesignPackage]:checked').parent().css({'background':'url("/Content/images/bgselected.jpg")', 'border-right':'none'});
		
		$('input[name=FinishPackage]').parent().css('background','');
		$('input[name=FinishPackage]:checked').parent().css({'background':'url("/Content/images/bgselected.jpg")', 'border-right':'none'});
		
        objPreInput.DesignPackage = $('#autocalc').find('input[name=DesignPackage]:checked').val();
		if(objPreInput.DesignPackage == 'trongoi') {
			$('#goiTK').html("trọn gói");
		}
		else if(objPreInput.DesignPackage == 'kientruc') {
			$('#goiTK').html("kiến trúc");
		}
		else if(objPreInput.DesignPackage == 'noithat') {
			$('#goiTK').html("nội thất");
		}
				
        objPreInput.FinishPackage = $('#autocalc').find('input[name=FinishPackage]:checked').val();
		if(objPreInput.FinishPackage == 'trungbinh') {
			$('#goiHT').html("gói trung bình");
		}
		else if(objPreInput.FinishPackage == 'kha') {
			$('#goiHT').html("gói khá");
		}
		else if(objPreInput.FinishPackage == 'caocap') {
			$('#goiHT').html("gói cao cấp");
		}
		else if(objPreInput.FinishPackage == 'sangtrong') {
			$('#goiHT').html("gói sang trọng");
		}
		
        objPreInput.FurniturePackage = $('#autocalc').find('input[name=FurniturePackage]:checked').val();
        objPreInput.DesignPriceForFloor = 0;
        objPreInput.DesignPriceForGarden = 0;
        objPreInput.DesignPrice = 0;
        objPreInput.ConstructPriceForFloor = 0;
        objPreInput.ConstructPrice = 0;
        objPreInput.FinshPrice = 0;
        objPreInput.FurniturePrice = 0;
        objPreInput.Fullname = $('#autocalc').find('input[name=Fullname]').val();
        objPreInput.Birth = $('#autocalc').find('input[name=Birth]').val();
        objPreInput.Gender = $('#autocalc').find('input[name=Gender]:checked').val();
        objPreInput.Email = $('#autocalc').find('input[name=Email]').val();
        objPreInput.Phone = $('#autocalc').find('input[name=Phone]').val();
        objPreInput.Address = $('#autocalc').find('input[name=Address]').val();
        objPreInput.Province_Code = $('#autocalc').find('input[name=Province_Code]:checked').val();
        objPreInput.Direction = $('#autocalc').find('select[name=Direction]').val();
        objPreInput.Type = $('#autocalc').find('input[name=Type]:checked').val();
        objPreInput.Status = $('#autocalc').find('input[name=Status]:checked').val();
        objPreInput.WeakGround = $('#autocalc').find('input[name=WeakGround]:checked').val();
        objPreInput.LandSize = parseFloat($('#autocalc').find('input[name=LandSize]').val());
        objPreInput.FloorSize = $('#autocalc').find('input[name=FloorSize]:checked').val();
        objPreInput.ConstructionSize = parseFloat($('#autocalc').find('input[name=ConstructionSize]').val());
        objPreInput.GardenSize = objPreInput.LandSize - objPreInput.ConstructionSize;
        objPreInput.SmallAlley = $('#autocalc').find('input[name=SmallAlley]:checked').val();
        objPreInput.Width = parseFloat($('#autocalc').find('input[name=Width]').val());
        objPreInput.Length = parseFloat($('#autocalc').find('input[name=Length]').val());
        objPreInput.Floor = parseFloat($('#autocalc').find('input[name=Floor]').val());
        objPreInput.BedRoom = parseFloat($('#autocalc').find('input[name=BedRoom]').val());
        objPreInput.Toilet = parseFloat($('#autocalc').find('input[name=Toilet]').val());
        objPreInput.Garage = $('#autocalc').find('input[name=Garage]:checked').val();
        objPreInput.Garden = $('#autocalc').find('input[name=Garden]:checked').val();
        objPreInput.CommonRoom = $('#autocalc').find('input[name=CommonRoom]:checked').val();
        objPreInput.Worship = $('#autocalc').find('input[name=Worship]:checked').val();
        objPreInput.Warehouse = $('#autocalc').find('input[name=Warehouse]:checked').val();
        objPreInput.Drying = $('#autocalc').find('input[name=Drying]:checked').val();
        objPreInput.Pool = $('#autocalc').find('input[name=Pool]:checked').val();
        objPreInput.Massage = $('#autocalc').find('input[name=Massage]:checked').val();
        objPreInput.Exercise = $('#autocalc').find('input[name=Exercise]:checked').val();
        objPreInput.Note = $('#autocalc').find('textarea[name=Note]').val();
		
		objPreInput.Quan = $('#autocalc').find('select[name=Quan]').val();
		objPreInput.DTngoaiLG = parseFloat($('#autocalc').find('input[name=DTngoaiLG]').val());
		objPreInput.Logioi = parseFloat($('#autocalc').find('input[name=Logioi]').val());
		objPreInput.ChieurongMT = parseFloat($('#autocalc').find('input[name=ChieurongMT]').val());
		objPreInput.QuanTrungTam = $('#autocalc').find('input[name=QuanTrungTam]:checked').val();
		objPreInput.DuongTMDV = $('#autocalc').find('input[name=DuongTMDV]:checked').val();
		objPreInput.ChieusauNgoaiLG = parseFloat($('#autocalc').find('input[name=ChieusauNgoaiLG]').val());
		objPreInput.DuongHem = $('#autocalc').find('input[name=DuongHem]:checked').val();
		objPreInput.HopKhoiKtr = $('#autocalc').find('input[name=HopKhoiKtr]:checked').val();
		objPreInput.XPXDimage = "";
		
		if(objPreInput.DTngoaiLG >= 150  && objPreInput.ChieurongMT >= 6.6) {
			$('#lodatlon').show();	
		}
		else {
			$('#lodatlon').hide();
		}
	
		if(objPreInput.Status == 'xaymoi') {
			$('#DesignPackage-trongoi').attr("disabled", "disabled");
			$('#DesignPackage-kientruc').attr("disabled", "disabled");
			$('#DesignPackage-noithat').attr("disabled", "disabled");
			
			$('#DesignPackage-caitaotrongoi').attr("disabled", "disabled");
			$('#DesignPackage-caitaokientruc').attr("disabled", "disabled");
			$('#DesignPackage-caitaonoithat').attr("disabled", "disabled");
			
			$('#DesignPackage-noithatnhanh').attr("disabled", "disabled");
			$('#DesignPackage-caitaonhanh').attr("disabled", "disabled");
			
			$('#DesignPackage-trongoi').removeAttr("disabled");
			$('#DesignPackage-kientruc').removeAttr("disabled");
			$('#DesignPackage-noithat').removeAttr("disabled");
			
			$('.disMoi').addClass("disabled").removeClass("pbhover");
			$('.disCaitao').addClass("disabled").removeClass("pbhover");
			$('.disNoithat').addClass("disabled").removeClass("pbhover");
			
			$('.disMoi').removeClass("disabled").addClass("pbhover");
			$(".statRegion").slideDown();
		}
		else if(objPreInput.Status == 'caitao') {
			$('#DesignPackage-trongoi').attr("disabled", "disabled");
			$('#DesignPackage-kientruc').attr("disabled", "disabled");
			$('#DesignPackage-noithat').attr("disabled", "disabled");
			
			$('#DesignPackage-caitaotrongoi').attr("disabled", "disabled");
			$('#DesignPackage-caitaokientruc').attr("disabled", "disabled");
			$('#DesignPackage-caitaonoithat').attr("disabled", "disabled");
			
			$('#DesignPackage-noithatnhanh').attr("disabled", "disabled");
			$('#DesignPackage-caitaonhanh').attr("disabled", "disabled");
			
			$('#DesignPackage-caitaotrongoi').removeAttr("disabled");
			$('#DesignPackage-caitaokientruc').removeAttr("disabled");
			$('#DesignPackage-caitaonoithat').removeAttr("disabled");
			
			$('#DesignPackage-noithatnhanh').removeAttr("disabled");
			$('#DesignPackage-caitaonhanh').removeAttr("disabled");
			
			$('.disMoi').addClass("disabled").removeClass("pbhover");
			$('.disCaitao').addClass("disabled").removeClass("pbhover");
			$('.disNoithat').addClass("disabled").removeClass("pbhover");
			
			$('.disCaitao').removeClass("disabled").addClass("pbhover");
			$(".statRegion").slideUp();
		}
		else if(objPreInput.Status == 'noithat') {
			$('#DesignPackage-trongoi').attr("disabled", "disabled");
			$('#DesignPackage-kientruc').attr("disabled", "disabled");
			$('#DesignPackage-noithat').attr("disabled", "disabled");
			
			$('#DesignPackage-caitaotrongoi').attr("disabled", "disabled");
			$('#DesignPackage-caitaokientruc').attr("disabled", "disabled");
			$('#DesignPackage-caitaonoithat').attr("disabled", "disabled");
			
			$('#DesignPackage-noithatnhanh').attr("disabled", "disabled");
			$('#DesignPackage-caitaonhanh').attr("disabled", "disabled");
			
			$('#DesignPackage-noithat').removeAttr("disabled");
			$('#DesignPackage-caitaotrongoi').removeAttr("disabled");
			$('#DesignPackage-caitaonoithat').removeAttr("disabled");
			
			$('.disMoi').addClass("disabled").removeClass("pbhover");
			$('.disCaitao').addClass("disabled").removeClass("pbhover");
			$('.disNoithat').addClass("disabled").removeClass("pbhover");
			
			$('.disNoithat').removeClass("disabled").addClass("pbhover");
			$(".statRegion").slideUp();
		}
		else {
			
		}

        //        $('.p-trongoi').find('.p-' + objPreInput.Province_Code).html(formatMoney(objPreInput.objPreDefine.giaThietke['trongoi'][objPreInput.Province_Code]));
        //        $('.p-kientruc').find('.p-' + objPreInput.Province_Code).html(formatMoney(objPreInput.objPreDefine.giaThietke['kientruc'][objPreInput.Province_Code]));
        //        $('.p-noithat').find('.p-' + objPreInput.Province_Code).html(formatMoney(objPreInput.objPreDefine.giaThietke['noithat'][objPreInput.Province_Code]));

        //        $('span.g').hide();
        //        $('.p-' + objPreInput.Province_Code).show();

        //        var construcMin = objPreInput.objPreDefine.giaPhantho['min'][objPreInput.Province_Code];
        //        var construcMin = objPreInput.objPreDefine.giaPhantho['max'][objPreInput.Province_Code];

        //        $('.p-construct-1').find('.p-' + objPreInput.Province_Code).html(formatMoney(objPreInput.objPreDefine.giaPhantho[objPreInput.Province_Code]));
        //        $('.p-construct-2').find('.p-' + objPreInput.Province_Code).html(formatMoney(objPreInput.objPreDefine.giaPhantho[objPreInput.Province_Code]));

        if (isNaN(objPreInput.GardenSize)) { objPreInput.GardenSize = 0; }
        if (objPreInput.GardenSize > 0) {
            $('.dtsanvuon').show();
            $('.dtsanvuon span').html(objPreInput.GardenSize);
        }
        else {
            $('.dtsanvuon').hide();
        }
    }
	
    $('#autocalc').find('input[name=Province_Code], input[name=Type], input[name=Status], input[name=ok], input[name=Finish]').click(function () { 
		okPreInput();
		if(objPreInput.Status == 'caitao' || objPreInput.Status == 'noithat') {
			hideStar(21); hideStar(22); hideStar(23); showStar(24); hideStar(25); hideStar(26); hideStar(27); hideStar(28);
			$('#DesignPackage-caitaotrongoi').attr('checked', 'checked');
		}
		else {
			showStar(21); hideStar(22); hideStar(23); hideStar(24); hideStar(25); hideStar(26); hideStar(27); hideStar(28);
			$('#DesignPackage-trongoi').attr('checked', 'checked');
		}		
		calcPrice();
	});
    $('input[name=reset]').click(function () {
        $('.price1').html('0 VND');
        $('.price2').html('0 VND');
    });
    $('#autocalc').find('input[name=LandSize], input[name=Width], input[name=Length], input[name=Floor], input[name=GardenSize], input[name=BedRoom], input[name=Toilet], input[name=DTngoaiLG], input[name=Logioi], input[name=ChieurongMT], input[name=ChieusauNgoaiLG]').keyup(function () { calcPrice(); });
	$('#autocalc').find('input[name=QuanTrungTam], input[name=DuongTMDV], input[name=DuongHem], input[name=HopKhoiKtr]').click(function () { calcPrice(); });
	$('#autocalc').find('input[name=ConstructionSize]').keyup(function () { 
		$('#autocalc').find('input[name=DTngoaiLG]').val($('#autocalc').find('input[name=ConstructionSize]').val());
		calcPrice(); 
	});
	
	var mp = new MathProcessor()
	
	$('#autocalc').find('input[name=Width], input[name=Length]').change(function () {
        $('input[name=ConstructionSize]').val(objPreInput.Width * objPreInput.Length);
        //        objPreInput.ConstructionSize = objPreInput.Width * objPreInput.Length;
        calcPrice();
    });

	$('#autocalc').find('input[name=LandSize], input[name=Width], input[name=Length], input[name=ConstructionSize], input[name=Floor], input[name=GardenSize], input[name=BedRoom], input[name=Toilet], input[name=DTngoaiLG], input[name=Logioi], input[name=ChieurongMT], input[name=ChieusauNgoaiLG]').change(function () {
		if(String($('#autocalc').find('input[name=LandSize]').val()) != "") { 
			$('#autocalc').find('input[name=LandSize]').val(mp.parse(String($('#autocalc').find('input[name=LandSize]').val())));
		}
		if(String($('#autocalc').find('input[name=ConstructionSize]').val()) != "") { 
			$('#autocalc').find('input[name=ConstructionSize]').val(mp.parse(String($('#autocalc').find('input[name=ConstructionSize]').val())));
		}
		if(String($('#autocalc').find('input[name=Floor]').val()) != "") { 
			$('#autocalc').find('input[name=Floor]').val(mp.parse(String($('#autocalc').find('input[name=Floor]').val())));
		}
		if(String($('#autocalc').find('input[name=BedRoom]').val()) != "") { 
			$('#autocalc').find('input[name=BedRoom]').val(mp.parse(String($('#autocalc').find('input[name=BedRoom]').val())));
		}
		if(String($('#autocalc').find('input[name=Toilet]').val()) != "") { 
			$('#autocalc').find('input[name=Toilet]').val(mp.parse(String($('#autocalc').find('input[name=Toilet]').val())));
		}
		if(String($('#autocalc').find('input[name=DTngoaiLG]').val()) != "") { 
			$('#autocalc').find('input[name=DTngoaiLG]').val(mp.parse(String($('#autocalc').find('input[name=DTngoaiLG]').val())));
		}
		if(String($('#autocalc').find('input[name=Logioi]').val()) != "") { 
			$('#autocalc').find('input[name=Logioi]').val(mp.parse(String($('#autocalc').find('input[name=Logioi]').val())));
		}
		if(String($('#autocalc').find('input[name=ChieurongMT]').val()) != "") { 
			$('#autocalc').find('input[name=ChieurongMT]').val(mp.parse(String($('#autocalc').find('input[name=ChieurongMT]').val())));
		}
		if(String($('#autocalc').find('input[name=ChieusauNgoaiLG]').val()) != "") { 
			$('#autocalc').find('input[name=ChieusauNgoaiLG]').val(mp.parse(String($('#autocalc').find('input[name=ChieusauNgoaiLG]').val())));
		}
		calcPrice(); 
	});
	
	$('#autocalc').find('select[name=Quan]').change(function () { 
		$('#autocalc').find('input[name=QuanTrungTam]').removeAttr('checked');
		calcPrice(); 
	});
    $('#autocalc').find('input[name=DesignPackage]').click(function () { calcPrice(); });
	$('#autocalc').find('input[name=FinishPackage]').click(function () { calcPrice(); });
    $('#autocalc').find('input[name=WeakGround], input[name=SmallAlley]').click(function () { calcPrice(); });

    var okPreInput = function () {
        changePreInput();
		$('.showHide').hide();
		$('.show'+objPreInput.Province_Code).show();
		$('.show'+objPreInput.DesignPackage).show();
		
        if (objPreInput.Province_Code == undefined) {
            $('.er-Province_Code').addClass('error'); return false;
        }
        else {
            $('.er-Province_Code').removeClass('error');
            $('.step1').css('display', 'block');
            //$('.step2').css('display', 'block');
			//$('.step3').css('display', 'block');
			$('.ts-type').slideDown();

			if(objPreInput.Province_Code == "HCM") {
				$('#taiHCM').slideDown(400, function() {
					$('#taiHCM').show();
				});
			}
			else {
				$('#taiHCM').slideUp(400, function() {
					$('#taiHCM').hide();
				});
			}			
        }

        if (objPreInput.Type == undefined) {
            $('.er-Type').addClass('error'); return false;
        }
        else {
            $('.er-Type').removeClass('error');
			$('.ts-status').slideDown();
        }

        if (objPreInput.Status == undefined) {
            $('.er-Status').addClass('error'); return false;
        }
        else {
            $('.er-Status').removeClass('error');
			$('.ts-size').slideDown();
        }

		if(objPreInput.Status == 'caitao' || objPreInput.Status == 'noithat'){
			//$('.t-kientruc').hide();
			//$('.t-noithat').hide();
			$('.ts-basic').slideUp();
			$('.ts-otherinfo').slideUp();
			$('.step2').css('display', 'none');
			$('.step3').css('display', 'none');
			$('.step4').css('display', 'none');
		}
		else if((objPreInput.Type == 'serviceA') || (objPreInput.Type == 'serviceB') || (objPreInput.Type == 'showroom')){
			$('.ts-basic').slideUp();
			$('.step2').css('display', 'block');
			$('.step3').css('display', 'none');
		}
		else {
			//$('.t-kientruc').show();
			//$('.t-noithat').show();
			$('.ts-otherinfo').slideDown();
			$('.ts-basic').slideDown();
			$('.step2').css('display', 'block');
			$('.step3').css('display', 'block');
			$('.step4').css('display', 'block');
		}
		
		if(objPreInput.Type == 'serviceA') {
			$('.showSanVuon').hide();
		}
		else {
			$('.showSanVuon').show();
		}
		
        $('.er-info').removeClass('error');
        if (isNaN(objPreInput.LandSize)) {
            $('.er-info').addClass('error'); return false;
        }

        if (isNaN(objPreInput.ConstructionSize)) {
            $('.er-info').addClass('error'); return false;
        }

        if (objPreInput.LandSize < objPreInput.ConstructionSize) {
            $('.er-info').addClass('error'); return false;
        }

        if (isNaN(objPreInput.Floor)) {
            $('.er-info').addClass('error'); return false;
        }

        if (objPreInput.LandSize - objPreInput.ConstructionSize < objPreInput.GardenSize) {
            $('.er-info').addClass('error'); return false;
        }
		//$('.ts-basic').slideDown();
		
        return true;
    }
    var calcPrice = function () {
        $('.price1').html('0 VND'); //se thay bang hinh loading
        $('.price2').html('0 VND'); //se thay bang hinh loading
        okPreInput();
        //if (okPreInput()) {
		$('#autocalc').submit(); //submit form qua ajax
        //$('#autocalc').trigger('submit'); //submit form qua ajax
        //}
    }
    //END: Code xu lu du lieu bao gia tu dong

    //BEGIN: Code hieu ung chuyen Tab cho phan bao gia tu dong
    var hideTab = function (index) {
        if ($('a.step' + index).hasClass('tabsel')) {
            $('a.step' + index).removeClass('tabsel');
        }
        $('#stepcontent' + index).slideUp();
    }
    var showTab = function (index) {
        if (!$('a.step' + index).hasClass('tabsel')) {
            $('a.step' + index).addClass('tabsel');
        }
        $('#stepcontent' + index).slideDown();
    }

    $('a.step0').click(function () {
        showTab(0); hideTab(1); hideTab(2); hideTab(3); hideTab(4); 
		calcPrice();
		return false;
    });
    $('a.step1, input[name=ok]').click(function () {
        if (okPreInput()) {
            $('#stepcontent1 .error').hide();
        }
        else {
            $('#stepcontent1 .error').show();
        }
        hideTab(0); showTab(1); hideTab(2); hideTab(3); hideTab(4); return false;
    });
    $('a.step2').click(function () {
        if (okPreInput()) {
            $('#stepcontent2 .error').hide();
        }
        else {
            $('#stepcontent2 .error').show();
        }
        hideTab(0); hideTab(1); showTab(2); hideTab(3); hideTab(4); return false;
    });
    $('a.step3').click(function () {
        if (okPreInput()) {
            $('#stepcontent3 .error').hide();
        }
        else {
            $('#stepcontent3 .error').show();
        }
        hideTab(0); hideTab(1); hideTab(2); showTab(3); hideTab(4); return false;
    });
    $('a.step4').click(function () {
        if (okPreInput()) {
            $('#stepcontent4 .error').hide();
        }
        else {
            $('#stepcontent4 .error').show();
        }
        hideTab(0); hideTab(1); hideTab(2); hideTab(3); showTab(4); return false;
    });
    //END: Code hieu ung chuyen Tab cho phan bao gia tu dong

    //BEGIN: Code hieu ung chuyen Tab cho phan hoan thien
    var hideStar = function (index) {
        $('.starcontent' + index).slideUp(400, function() {
			$('.starcontent' + index).hide();
		});
    }
    var showStar = function (index) {
        $('.starcontent' + index).slideDown(400, function() {
			$('.starcontent' + index).show();
		});
    }

    $('#Finish-star1').click(function () {
        showStar(1); hideStar(2); hideStar(3); hideStar(4); hideStar(5);
    });
    $('#Finish-star2').click(function () {
        hideStar(1); showStar(2); hideStar(3); hideStar(4); hideStar(5);
    });
    $('#Finish-star3').click(function () {
        hideStar(1); hideStar(2); showStar(3); hideStar(4); hideStar(5);
    });
    $('#Finish-star4').click(function () {
        hideStar(1); hideStar(2); hideStar(3); showStar(4); hideStar(5);
    });
    $('#Finish-star5').click(function () {
        hideStar(1); hideStar(2); hideStar(3); hideStar(4); showStar(5);
    });
	
    $('#DesignPackage-trongoi').click(function () {
        showStar(21); hideStar(22); hideStar(23); hideStar(24); hideStar(25); hideStar(26); hideStar(27); hideStar(28);
    });
    $('#DesignPackage-kientruc').click(function () {
        hideStar(21); showStar(22); hideStar(23); hideStar(24); hideStar(25); hideStar(26); hideStar(27); hideStar(28);
    });
    $('#DesignPackage-noithat').click(function () {
        hideStar(21); hideStar(22); showStar(23); hideStar(24); hideStar(25); hideStar(26); hideStar(27); hideStar(28);
    });
    $('#DesignPackage-caitaotrongoi').click(function () {
        hideStar(21); hideStar(22); hideStar(23); showStar(24); hideStar(25); hideStar(26); hideStar(27); hideStar(28);
    });
    $('#DesignPackage-caitaokientruc').click(function () {
        hideStar(21); hideStar(22); hideStar(23); hideStar(24); showStar(25); hideStar(26); hideStar(27); hideStar(28);
    });
	$('#DesignPackage-caitaonoithat').click(function () {
        hideStar(21); hideStar(22); hideStar(23); hideStar(24); hideStar(25); showStar(26); hideStar(27); hideStar(28);
    });
    $('#DesignPackage-noithatnhanh').click(function () {
        hideStar(21); hideStar(22); hideStar(23); hideStar(24); hideStar(25); hideStar(26); showStar(27); hideStar(28);
    });
    $('#DesignPackage-caitaonhanh').click(function () {
        hideStar(21); hideStar(22); hideStar(23); hideStar(24); hideStar(25); hideStar(26); hideStar(27); showStar(28);
    });
	
    //END: Code hieu ung chuyen Tab cho phan hoan thien

    $("#starcontent1 .price-calc1-sum").html(function () {
        var a = 0;
        $("#starcontent1 .price-calc1").each(function () {
            a += parseInt($(this).html().replace(/,/gi, ""));
        });
        return formatMoney(a);
    });
    $("#starcontent1 .price-calc2-sum").html(function () {
        var a = 0;
        $("#starcontent1 .price-calc2").each(function () {
            a += parseInt($(this).html().replace(/,/gi, ""));
        });
        return formatMoney(a);
    });

    $("#starcontent2 .price-calc1-sum").html(function () {
        var a = 0;
        $("#starcontent2 .price-calc1").each(function () {
            a += parseInt($(this).html().replace(/,/gi, ""));
        });
        return formatMoney(a);
    });
    $("#starcontent2 .price-calc2-sum").html(function () {
        var a = 0;
        $("#starcontent2 .price-calc2").each(function () {
            a += parseInt($(this).html().replace(/,/gi, ""));
        });
        return formatMoney(a);
    });

    $("#starcontent3 .price-calc1-sum").html(function () {
        var a = 0;
        $("#starcontent3 .price-calc1").each(function () {
            a += parseInt($(this).html().replace(/,/gi, ""));
        });
        return formatMoney(a);
    });
    $("#starcontent3 .price-calc2-sum").html(function () {
        var a = 0;
        $("#starcontent3 .price-calc2").each(function () {
            a += parseInt($(this).html().replace(/,/gi, ""));
        });
        return formatMoney(a);
    });

    $("#starcontent4 .price-calc1-sum").html(function () {
        var a = 0;
        $("#starcontent4 .price-calc1").each(function () {
            a += parseInt($(this).html().replace(/,/gi, ""));
        });
        return formatMoney(a);
    });
    $("#starcontent4 .price-calc2-sum").html(function () {
        var a = 0;
        $("#starcontent4 .price-calc2").each(function () {
            a += parseInt($(this).html().replace(/,/gi, ""));
        });
        return formatMoney(a);
    });

    $("#starcontent5 .price-calc1-sum").html(function () {
        var a = 0;
        $("#starcontent5 .price-calc1").each(function () {
            a += parseInt($(this).html().replace(/,/gi, ""));
        });
        return formatMoney(a);
    });
    $("#starcontent5 .price-calc2-sum").html(function () {
        var a = 0;
        $("#starcontent5 .price-calc2").each(function () {
            a += parseInt($(this).html().replace(/,/gi, ""));
        });
        return formatMoney(a);
    });
});
