//********* plugin for sliderImg *********
/***************************************************************
 * Coder: Phan Nguyên Như (nhuphan@vietmeta.com, +84986030757) *
 ***************************************************************/
var i = new Image();
$.fn.image = function (src, id, catSlug, target) {
    return this.each(function () {
        i = new Image();
        i.src = src;
        i.onload = function () {
            var detailsLink = '#';
            if (catSlug == "bietthu") {
                detailsLink = '/NhaDep/BietThu/' + id + '.html#' + curImg;
            }
            else if (catSlug == "nhapho") {
                detailsLink = '/NhaDep/NhaPho/' + id + '.html#' + curImg;
            }
			else if (catSlug == "canho,penthouse") {
                detailsLink = '/NhaDep/CanHo-Penthouse/' + id + '.html#' + curImg;
            }
            else if (catSlug == "khachsan,resort") {
                detailsLink = '/KhachSan-Resort/' + id + '.html#' + curImg;
            }
            else if (catSlug == "spa,beautysalon") {
                detailsLink = '/Spa-BeautySalon/' + id + '.html#' + curImg;
            }
            else if (catSlug == "bar,cafe,nhahang,karaoke") {
                detailsLink = '/Bar-Cafe-NhaHang-Karaoke/' + id + '.html#' + curImg;
            }
            else if (catSlug == "showroom,shop,vanphong") {
                detailsLink = '/ShowRoom-Shop-VanPhong/' + id + '.html#' + curImg;
            }
			else if (catSlug == "photos") {
                detailsLink = '/Photos/' + id + '.html#' + curImg;
            }

            $("#sliderImg, #sliderImgDetails").html('');
            if (detailsLink != '#') {
                $("#sliderImg, #sliderImgDetails").html('<a href="' + detailsLink + '" target="' + target + '"><img id="fullpic" class="captify" src="' + src + '" rel="typo" alt="" /></a>');
            }
            else {
                $("#sliderImg, #sliderImgDetails").html('<img id="fullpic" class="captify" src="' + src + '" rel="typo" alt="" />');
            }

            jQuery('<div/>', { id: 'typo' }).appendTo('#sliderImg, #sliderImgDetails');
            if ((window.location.pathname.substr(window.location.pathname.length - 5) != '.html') && (detailsLink != '#')) {
                $("#typo").html('<a href="' + detailsLink + '" target="' + target + '" style="display:block;width:100%;">' + mycarousel_itemList[curImg].desc + '<br />' + mycarousel_itemList[curImg].addr + '<br /><span style="color:red;">Bấm vào ảnh để xem thêm chi tiết về dự án</span></a>');
            }
            else {
                $("#typo").html(mycarousel_itemList[curImg].desc + '<br />' + mycarousel_itemList[curImg].addr + '<br /><a href="/NoiThat/love" target="_blank"><span style="color:#00ff00;">Bấm vào SHOP NỘI THẤT để xem chi tiết và giá sản phẩm của Wonder</span></a>');
            }
            $('img.captify').captify({
                // all of these options are... optional
                // ---
                // speed of the mouseover effect
                speedOver: 'fast',
                // speed of the mouseout effect
                speedOut: 'normal',
                // how long to delay the hiding of the caption after mouseout (ms)
                hideDelay: 500,
                // 'fade', 'slide', 'always-on'
                animation: 'slide',
                // text/html to be placed at the beginning of every caption
                prefix: '',
                // opacity of the caption on mouse over
                opacity: '0.7',
                // the name of the CSS class to apply to the caption box
                className: 'caption-bottom',
                // position of the caption (top or bottom)
                position: 'bottom',
                // caption span % of the image
                spanWidth: '95%',
				width: $("#sliderImg").width(), //Nhu them vao vi Bi Adblock Plus chan
				height: $("#sliderImg").height() //Nhu them vao vi Bi Adblock Plus chan
            });
            $("#fullpic").hide();
            $("#fullpic").fadeIn();
        }
    });
}

function mycarousel_itemLoadCallback(carousel, state) {
	for (var i = carousel.first; i <= carousel.last; i++) {
		if (carousel.has(i)) {
			continue;
		}
		if (i > mycarousel_itemList.length) {
			break;
		}
		
		carousel.add(i, mycarousel_getItemHTML(mycarousel_itemList[i-1], i-1));
		myJQuery();
	}
};

function mycarousel_getItemHTML(item, i) {
    return '<a href="#' + i + '" alt="' + item.id + '"><img src="/Images/' + item.w + 'x' + item.h + '/' + item.imgname + '" alt="" nopin="nopin" /></a>';
};

function loadImg(_curImg) {
    $("#sliderImg").html('<p class="loading"><img src="/Content/images/loading.gif" nopin="nopin" /></p>');
    var fWidth = 672;
    var fHeight = 375;
    if (mycarousel_itemList[_curImg].catSlug == 'nhapho') {
        fWidth = 445;
        fHeight = 602;
    }

    $("#sliderImg").image('/Images/' + fWidth + 'x' + fHeight + '/' + mycarousel_itemList[_curImg].imgname, mycarousel_itemList[_curImg].id, mycarousel_itemList[_curImg].catSlug, mycarousel_itemList[_curImg].target);
    //$("#details").html('<a href="/'+module+'/'+mycarousel_itemList[_curImg].id+'">Xem chi tiết »</a>');	
}
function loadImgDetails(_curImg) {
    $("#sliderImgDetails").html('<p class="loading"><img src="/Content/images/loading.gif" nopin="nopin" /></p>');
    var fWidth = 672;
    var fHeight = 375;
    if (mycarousel_itemList[_curImg].catSlug == 'nhapho') {
        fWidth = 445;
        fHeight = 602;
    }

    $("#sliderImgDetails").image('/Images/' + fWidth + 'x' + fHeight + '/' + mycarousel_itemList[_curImg].imgname, mycarousel_itemList[_curImg].id, mycarousel_itemList[_curImg].catSlug, mycarousel_itemList[_curImg].target);
    //$("#details").html('<a href="/'+module+'/'+mycarousel_itemList[_curImg].id+'">Xem chi tiết »</a>');	
}

function myJQuery() {
	$("#scroll div ul li a").click(function(){
		//Load image on click thumb
		curImg = parseInt($(this).attr('href').replace("#",""));
		curID = parseInt($(this).attr('alt'));
		
		if($("#sliderImgDetails").length > 0) {
			loadImgDetails(curImg);
		} else {
			loadImg(curImg);
		}
		$("#related-body").slideUp();
	})
};

jQuery(document).ready(function() {
	jQuery('#scroll').jcarousel({
		//scroll: Math.ceil($(window).width()/260)-1,
		scroll: 3,
		animation: "fast",
		wrap: "both",
		size: mycarousel_itemList.length,
		itemLoadCallback: {onBeforeAnimation: mycarousel_itemLoadCallback},
		buttonNextHTML: '<div><div class="btn"></div></div>',
		buttonPrevHTML: '<div><div class="btn"></div></div>'
	});
	
	//if(needloadImg) {
		//Load image on page load
		var arHref = window.location.href.split("#");
		curImg=parseInt(arHref[1]);
		if(isNaN(curImg)){curImg=0;}

		//curImg=Math.floor(Math.random()*mycarousel_itemList.length)
		loadImg(curImg);
	//}
});

