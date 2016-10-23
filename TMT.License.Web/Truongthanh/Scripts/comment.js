/***************************************************************
 * Coder: Phan Nguyên Như (nhuphan@vietmeta.com, +84986030757) *
 ***************************************************************/
var dateFormat = function() {
	var	token = /d{1,4}|m{1,4}|yy(?:yy)?|([HhMsTt])\1?|[LloSZ]|"[^"]*"|'[^']*'/g,
		timezone = /\b(?:[PMCEA][SDP]T|(?:Pacific|Mountain|Central|Eastern|Atlantic) (?:Standard|Daylight|Prevailing) Time|(?:GMT|UTC)(?:[-+]\d{4})?)\b/g,
		timezoneClip = /[^-+\dA-Z]/g,
		pad = function (val, len) {
			val = String(val);
			len = len || 2;
			while (val.length < len) val = "0" + val;
			return val;
		};

	// Regexes and supporting functions are cached through closure
	return function (date, mask, utc) {
		var dF = dateFormat;

		// You can't provide utc if you skip other args (use the "UTC:" mask prefix)
		if (arguments.length == 1 && Object.prototype.toString.call(date) == "[object String]" && !/\d/.test(date)) {
			mask = date;
			date = undefined;
		}

		// Passing date through Date applies Date.parse, if necessary
		date = date ? new Date(date) : new Date;
		if (isNaN(date)) throw SyntaxError("invalid date");

		mask = String(dF.masks[mask] || mask || dF.masks["default"]);

		// Allow setting the utc argument via the mask
		if (mask.slice(0, 4) == "UTC:") {
			mask = mask.slice(4);
			utc = true;
		}

		var	_ = utc ? "getUTC" : "get",
			d = date[_ + "Date"](),
			D = date[_ + "Day"](),
			m = date[_ + "Month"](),
			y = date[_ + "FullYear"](),
			H = date[_ + "Hours"](),
			M = date[_ + "Minutes"](),
			s = date[_ + "Seconds"](),
			L = date[_ + "Milliseconds"](),
			o = utc ? 0 : date.getTimezoneOffset(),
			flags = {
				d:    d,
				dd:   pad(d),
				ddd:  dF.i18n.dayNames[D],
				dddd: dF.i18n.dayNames[D + 7],
				m:    m + 1,
				mm:   pad(m + 1),
				mmm:  dF.i18n.monthNames[m],
				mmmm: dF.i18n.monthNames[m + 12],
				yy:   String(y).slice(2),
				yyyy: y,
				h:    H % 12 || 12,
				hh:   pad(H % 12 || 12),
				H:    H,
				HH:   pad(H),
				M:    M,
				MM:   pad(M),
				s:    s,
				ss:   pad(s),
				l:    pad(L, 3),
				L:    pad(L > 99 ? Math.round(L / 10) : L),
				t:    H < 12 ? "a"  : "p",
				tt:   H < 12 ? "am" : "pm",
				T:    H < 12 ? "A"  : "P",
				TT:   H < 12 ? "AM" : "PM",
				Z:    utc ? "UTC" : (String(date).match(timezone) || [""]).pop().replace(timezoneClip, ""),
				o:    (o > 0 ? "-" : "+") + pad(Math.floor(Math.abs(o) / 60) * 100 + Math.abs(o) % 60, 4),
				S:    ["th", "st", "nd", "rd"][d % 10 > 3 ? 0 : (d % 100 - d % 10 != 10) * d % 10]
			};

		return mask.replace(token, function ($0) {
			return $0 in flags ? flags[$0] : $0.slice(1, $0.length - 1);
		});
	};
}();

// Some common format strings
dateFormat.masks = {
	"default":      "ddd mmm dd yyyy HH:MM:ss",
	shortDate:      "m/d/yy",
	mediumDate:     "mmm d, yyyy",
	longDate:       "mmmm d, yyyy",
	fullDate:       "dddd, mmmm d, yyyy",
	shortTime:      "h:MM TT",
	mediumTime:     "h:MM:ss TT",
	longTime:       "h:MM:ss TT Z",
	isoDate:        "yyyy-mm-dd",
	isoTime:        "HH:MM:ss",
	isoDateTime:    "yyyy-mm-dd'T'HH:MM:ss",
	isoUtcDateTime: "UTC:yyyy-mm-dd'T'HH:MM:ss'Z'"
};

// Internationalization strings
dateFormat.i18n = {
	dayNames: [
		"Sun", "Mon", "Tue", "Wed", "Thu", "Fri", "Sat",
		"Sunday", "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday"
	],
	monthNames: [
		"Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec",
		"January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December"
	]
};

// For convenience...
Date.prototype.format = function (mask, utc) {
	return dateFormat(this, mask, utc);
};

Date.prototype.timeAgo = function () {
	var date = this,
	diff = (((new Date()).getTime() - date.getTime()) / 1000),
	day_diff = Math.floor(diff / 86400);

	if ( isNaN(day_diff) || day_diff < 0 || day_diff >= 31 )
	return date.format("h:MM TT ngày d/m/yyyy");

	return day_diff == 0 && (
		diff < 60 && "cách đây vài giây" ||
		diff < 120 && "khoảng một phút trước" ||
		diff < 3600 && Math.floor( diff / 60 ) + " phút trước" ||
		diff < 7200 && "khoảng một giờ trước" ||
		diff < 86400 && Math.floor( diff / 3600 ) + " giờ trước") ||
		day_diff == 1 && "hôm qua" ||
		day_diff < 7 && day_diff + " ngày trước" ||
		day_diff < 31 && Math.ceil( day_diff / 7 ) + " tuần trước";
};

var jsPostIds = {}; //string: jsPostIds = { 'p0':'12,44,45' }
var jsCommentData = {}; //string: jsCommentData = { 'p23':[111,244,145], 'p25':[115,214,165] }
var jsRecentCommentData = {'recentComment':[]};
var isManager = false;

var htmlEncode = function(value) {
  return $('<div/>').text(value).html();
}

var htmlDecode = function(value) {
  return $('<div/>').html(value).text();
}

$(document).ready(function () {
	if($("#fb-root-comment").length != 0) {
		//============= B: Facebook ================
		(function() {
			var e = document.createElement('script'); e.async = true;
			e.src = document.location.protocol + '//connect.facebook.net/en_US/all.js';
			document.getElementById('fb-root-comment').appendChild(e);
		}());
		//============= B: Facebook ================
		
		var playDing = function(okDing){
			if(okDing) {
				$('.audioDing').remove();
				$("<audio></audio>").attr({
					'class':'audioDing',
					'src':'http://wonder.vn/Content/audio/ding.mp3',
					'volume':0.4,
					'autoplay':'autoplay'
				}).appendTo("body");
			};
		};

		var loadPostComment = function(jsCommentData, howTo, okDing) {
			if($('.comment-list').length > 0) {
				$.getJSON('http://wonder.vn/Comment/AjaxLoad/?howTo=1', jsPostIds, function(data) {
					var curComment;
					$.each(data, function(postID, listObjComment) {
						var curItems = [];
						$.each(listObjComment, function(index, objComment) {
							curItems.push(objComment['ID']);
							if ($('#comment-' + objComment['ID']).length == 0) {
								var date = new Date(parseInt(objComment['PostTime'].substr(6)));
								$('<li id="comment-' + objComment['ID'] + '"><a href="#" class="link-poster">' + ((objComment['PosterFullname'] != '') ? objComment['PosterFullname'] : objComment['Poster']) + '</a> ' + htmlEncode(objComment['Comment']) + '<div id="cdel-' + objComment['ID'] + '" class="cdel"></div><br /><span class="time">' + date.timeAgo() + '</span></li>').hide().insertBefore('#input-block-' + postID).slideDown();
								if(isManager) {
									$('#cdel-' + objComment['ID']).click(function() {
										$.getJSON('http://wonder.vn/Comment/AjaxDelete/' + objComment['ID'], function(data) {
											if(data) {
												$('#comment-' + objComment['ID']).slideUp("normal", function() { $(this).remove(); });
											};
										});
									});
								};
							};
							curComment = $('#comment-' + objComment['ID']);
						});
						var oldItems = jsCommentData['p' + postID];
						$.each(oldItems, function(key, cID) {
							if(curItems.indexOf(cID) < 0) {
								$('#comment-' + cID).slideUp("normal", function() { $(this).remove(); });
							};
						});
						jsCommentData['p' + postID] = curItems;
					});
				});
			};
			
			if($('.recent-comment-list').length > 0) {
				//Load recent-comment
				$.getJSON('http://wonder.vn/Comment/AjaxLoadRecent/?Skip=0&Take=50', function(data) {
					var curRecentComment;
					$.each(data, function(keyBlock, listObjComment) {
						var curRecentItems = [];
						$.each(listObjComment, function(index, objComment) {
							curRecentItems.push(objComment['ID']);
							if ($('#recent-comment-' + objComment['ID']).length == 0) {
								var date = new Date(parseInt(objComment['PostTime'].substr(6)));
								$('<li id="recent-comment-' + objComment['ID'] + '"><a href="#" class="link-poster">' + ((objComment['PosterFullname'] != '') ? objComment['PosterFullname'] : objComment['Poster']) + '</a> : <a href="/Blog/DuAn/' + objComment['RootPost_ID'] + '.html" class="link-post">' + htmlEncode(objComment['RootPost_Title']) + '</a> ➥ <a href="/Blog/DuAn/' + objComment['RootPost_ID'] + '.html#image-' + objComment['Post_ID'] + '" class="link-post">' + htmlEncode(objComment['Post_Title']) + '</a><div id="recent-cdel-' + objComment['ID'] + '" class="recent-cdel"></div><br />' + htmlEncode(objComment['Comment']) + '<br /><span class="time">' + date.timeAgo() + '</span></li>').hide().insertAfter('#recent-input-block').slideDown();
								playDing(okDing);
								if(isManager) {
									$('#recent-cdel-' + objComment['ID']).click(function() {
										$.getJSON('http://wonder.vn/Comment/AjaxDelete/' + objComment['ID'], function(data) {
											if(data) {
												$('#recent-comment-' + objComment['ID']).slideUp("normal", function() { $(this).remove(); });
											};
										});
									});
								};
							};
							curRecentComment = $('#recent-comment-' + objComment['ID']);
						});
						var oldItems = jsRecentCommentData['recentComment'];
						$.each(oldItems, function(key, cID) {
							if(curRecentItems.indexOf(cID) < 0) {
								$('#recent-comment-' + cID).slideUp("normal", function() { $(this).remove(); });
							};
						});
						jsRecentCommentData['recentComment'] = curRecentItems;
					});
				});
			};
			//Process to insert new comment for images
			var t=setTimeout(function(){loadPostComment(jsCommentData, 0, true)}, 5000);		
		};

		var postMessage = function(container) {
			var Post_ID = container.find('.comment-id').val();
			var commentText = container.find('.comment-input').val();
			var imgNeo = '#image-' + Post_ID;
			var postComment = { 'Post_ID':Post_ID, 'commentText':commentText };
			$.getJSON('http://wonder.vn/Comment/AjaxPost', postComment, function(data) {
				container.find('.comment-input').val('');
				var date = new Date().getTime();;
				$('<li id="comment-' + data['ID'] + '"><a href="#" class="link-poster">' + ((data['PosterFullname'] != '') ? data['PosterFullname'] : data['Poster']) + '</a> ' + htmlEncode(data['Comment']) + '<div id="cdel-' + data['ID'] + '" class="cdel"></div><br /><span class="time">' + date.timeAgo() + '</span></li>').hide().insertBefore(container).slideDown();
				if(isManager) {
					$('#cdel-' + data['ID']).click(function() {
						$.getJSON('http://wonder.vn/Comment/AjaxDelete/' + data['ID'], function(dataJs) {
							if(dataJs) {
								$('#comment-' + data['ID']).slideUp("normal", function() { $(this).remove(); });
							};
						});
					});
				};
			});
			setTimeout(function(){setStatus(document.URL.replace(imgNeo, '') + imgNeo + ' - ' + commentText)}, 2000);
		};
		
		//============== B: Run ====================
		loadPostComment(jsCommentData, 0, false);

		$('.comment-input').keypress(function(event) {
			if (event.which == 13) {
				var container = $(this).parent();
				postMessage(container);
			};
		});
		
		$('.comment-submit').click(function() {
			var container = $(this).parent();
			postMessage(container);
		});	
	}
	//============== E: Run ====================
});