/***************************************************************
 * Coder: Phan Nguyên Như (nhuphan@vietmeta.com, +84986030757) *
 ***************************************************************/
$(document).ready(function () {
	var saigon3Day = [2, 4, 6];
	var saigon4Day = [1, 3, 5];
	var d = new Date();
	var n = d.getDay();
	var h = d.getHours();
	if($.inArray(n, saigon3Day) != -1) {
		//$('.hotline').html('Hotline: 090 5222225');
		$('.pos1').html($('.contact-quan3').html());
		$('.pos2').html($('.contact-quan4').html());
	}
	else if($.inArray(n, saigon4Day) != -1) {
		//$('.hotline').html('Hotline: 090 5222225');
		$('.pos1').html($('.contact-quan4').html());
		$('.pos2').html($('.contact-quan3').html());
	}
	else {
		//$('.hotline').html('Hotline: 090 5222225');
		if(h<12) {
			$('.pos1').html($('.contact-quan4').html());
			$('.pos2').html($('.contact-quan3').html());
		} else {
			$('.pos1').html($('.contact-quan3').html());
			$('.pos2').html($('.contact-quan4').html());
		}
	}
	$('a.lightBox').lightBox();
    $('#ContactDesign').find('select.locationSelectBox').MakeVmSelectbox({ className: 'locationSelectBox' });
    $('#ContactDesign').find('select#Direction').MakeVmSelectbox({ width: '152', className: 'directionSelectBox' });
    var showOption = function () {
        if (($('#ContactDesign').find('input[name="Type"]:checked').val() == 'nhapho') || ($('#ContactDesign').find('input[name="Type"]:checked').val() == 'bietthu')) {
            $('#ContactDesign').find('.option').slideDown();
            showingOption = true;
        }
        else {
            $('#ContactDesign').find('.option').slideUp();
        }
    }
    var showingOption = false;
    $('#ContactDesign').find('input[name="Type"]').change(function () {
        $('#ContactDesign').find('.Status').slideDown();
        if (($('#ContactDesign').find('input[name="Status"]:checked').val() == 'xaymoi') || ($('#ContactDesign').find('input[name="Status"]:checked').val() == 'caitao')) {
            showOption();
        }
    });
    $('#ContactDesign').find('input[name="Status"]').change(function () {
        $('.info').slideDown();
        showOption();
        if ($('#ContactDesign').find('input[name="Status"]:checked').val() == 'xaymoi') {
            $('#ContactDesign').find('.other').slideDown();
            $('#ContactDesign').find('.mong').slideDown();
        }
        else {
            $('#ContactDesign').find('input[name="WeakGround"]').removeAttr("checked");
            //$('#ContactDesign').find('input[name="SmallAlley"]').removeAttr("checked");
            //$('#ContactDesign').find('.other').slideUp();
            $('#ContactDesign').find('.mong').slideUp();
        }
    });

    //BEGIN Contact Form --------------------------------
	$('#ContactDesign').submit(function () {
		var options = {
			beforeSubmit: showRequestContact,
			success: showResponseContact,
			dataType: 'html'
		};
		$(this).ajaxSubmit(options);
		return false;
	});

    function showRequestContact(formData, jqForm, options) {
        $("#notice-submitting").show();
        //var queryString = $.param(formData);
        //alert('About to submit: \n\n' + queryString);
        return true;
    }

    function showResponseContact(responseText, statusText, xhr, $form) {
		var jsonRes = JSON.parse(responseText);
        $("#notice-submitting").hide();
        if (jsonRes["resultOK"] != undefined) {
			$('#ContactDesign')[0].reset();
            alert("Đã gởi nội dung liên hệ thành công!\r\nCảm ơn bạn đã liên hệ Wonder-design.");
        }
        else if ((jsonRes == undefined) || (jsonRes == null) || (jsonRes == '') || (jsonRes.length == 0)) {
            alert("Chưa gởi được liên hệ.\r\nCó thể do máy chủ bận, vui lòng liên hệ vào lúc khác.");
        }
        else {
            alert("Bạn nhập thiếu thông tin.\r\nVui lòng điền đủ thông tin yêu cầu!");
        }

        if (jsonRes["Fullname"] != undefined) {
            $('label[for="Fullname"]').addClass('error').attr('title', jsonRes["Fullname"]);
        }
        else {
            $('label[for="Fullname"]').removeClass('error').attr('title', '');
        }

        if (jsonRes["Email"] != undefined) {
            $('label[for="Email"]').addClass('error').attr('title', jsonRes["Email"]);
        }
        else {
            $('label[for="Email"]').removeClass('error').attr('title', '');
        }

        if (jsonRes["Phone"] != undefined) {
            $('label[for="Phone"]').addClass('error').attr('title', jsonRes["Phone"]);
        }
        else {
            $('label[for="Phone"]').removeClass('error').attr('title', '');
        }		

        if (jsonRes["Gender"] != undefined) {
            $('label[for="nam"]').addClass('error').attr('title', jsonRes["Gender"]);
            $('label[for="nu"]').addClass('error').attr('title', jsonRes["Gender"]);
        }
        else {
            $('label[for="nam"]').removeClass('error').attr('title', '');
            $('label[for="nu"]').removeClass('error').attr('title', '');
        }
    }
    //END Contact Form --------------------------------

    $('#sliderImg').innerfade({
        speed: 1000,
        timeout: 3000
    });

    //----- BEGIN Multi upload cho việc gởi email liên hệ -----
    var fileMax = 5;
	var indexFile = 1;
    var lastFile = "";

    $('input.multiupload').before('<div id="files_list" style="border:1px solid #666;padding:3px;margin-bottom:10px;font-size:x-small;width:441px;"><span style="margin-left:5px;font-size:12px;"><class="t">Danh sách file (tối đa ' + fileMax + '):</class></span></div>');

    $("input.multiupload").change(function () {
        doIt($(this), fileMax);
    });

    function doIt(obj, fm) {
        var v = obj.val();
        if ((v != '') && (v != null) && (v != lastFile)) {
            lastFile = v;
            if ($('input.multiupload').size() > fm) {
                alert('Chỉ đính kèm tối đa ' + fm + ' file trong mỗi lần liên hệ!');
                obj.val('');
                return false;
            }
            obj.hide();
            var newUploadElement = $('<input />', { className: 'multiupload', type: 'file', name: 'Attachments[' + indexFile++ + ']' });
            obj.after(newUploadElement);
            newUploadElement.change(function () {
                doIt($(this), fm)
            });

            $("div#files_list").append('<span style="margin-left:10px;font-size:12px;"><br />- ' + v + ' [<a href="#" style="color:red;">Xóa</a>]</span>').find("a").click(function () {
                $(this).parent().remove();
                obj.remove();
                return false;
            });
        }
    };

    //----- END Multi upload cho việc gởi email liên hệ -----
	
    $('.boxshadow, .boxshadow-1').animate({ boxShadow: '-1 1 3px #999' });
    $('.boxshadow, .boxshadow-1').hover(
        function () { $(this).animate({ boxShadow: '-1 1 6px #333' }, 100); },
        function () { $(this).animate({ boxShadow: '-1 1 3px #999' }, 100); }
    );
	$('.print_area').animate({ boxShadow: '-1 1 6px #999' });
    $('.print_area').hover(
        function () { $(this).animate({ boxShadow: '-1 1 9px #333' }, 100); },
        function () { $(this).animate({ boxShadow: '-1 1 9px #999' }, 100); }
    );
    //$('#v-menu li').animate({ boxShadow: '-1 1 3px #111' });
    //$('#v-menu li').hover(
    //    function () { $(this).animate({ boxShadow: '-1 1 6px #000' }, 100); },
    //    function () { $(this).animate({ boxShadow: '-1 1 3px #111' }, 100); }
    //);
	
	var pageOrder = function() {
		for (var i=0; i<arguments.length; i++) {
			$(".paper" + arguments[i]).css('z-index' , 900 - i);
		}
	};
	var showPrePage = function() {
		for (var i=0; i<arguments.length; i++) {
			if(arguments[i] == 1) {
				$(".paper" + (i+1) + " .sotrangtrenPre").show();
				$(".paper" + (i+1) + " .sotrangduoiPre").show();
			}
			else {
				$(".paper" + (i+1) + " .sotrangtrenPre").hide();
				$(".paper" + (i+1) + " .sotrangduoiPre").hide();
			}
		}
	};
	
	showPrePage(0,0,0,0,0);
	$('.paper1').click(function() {
		pageOrder(1,2,3,4,5);
		showPrePage(0,0,0,0,0);
	});
	$('.paper2').click(function() {
		pageOrder(2,1,3,4,5);
		showPrePage(1,0,0,0,0);
	});
	$('.paper3').click(function() {
		pageOrder(3,2,1,4,5);
		showPrePage(1,1,0,0,0);
	});
	$('.paper4').click(function() {
		pageOrder(4,3,2,1,5);
		showPrePage(1,1,1,0,0);
	});
	$('.paper5').click(function() {
		pageOrder(5,4,3,2,1);
		showPrePage(1,1,1,1,0);
	});
	$('#privateshow').click(function() {
		$('.private').slideDown();
		return false;
	});
});