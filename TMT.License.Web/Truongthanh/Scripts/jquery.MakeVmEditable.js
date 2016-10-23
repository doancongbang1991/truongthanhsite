/***************************************************************
 * Coder: Phan Nguyên Như (nhuphan@vietmeta.com, +84986030757) *
 ***************************************************************/
function HtmlEncode(s) {
    var el = document.createElement("div");
    el.innerText = el.textContent = s;
    s = el.innerHTML;
    delete el;
    return s;
}
function repeat(str, i) {
    if (isNaN(i) || i <= 0) return "";
    return str + repeat(str, i - 1);
}

(function ($) {
	$.fn.UpdateInput = function (value) {
		if($(this).is(':radio')) {
			$(this).each(function() {
				if($(this).val() == value) {
					$(this).attr('checked', 'checked');
				}
				else {
					$(this).removeAttr('checked');
				}
			});
		}
		else if($(this).is(':checkbox')) {
			alert('checkbox');
		}
		else if($(this).is('select')) {
			alert('select');
		}
		else if($(this).is('textarea')) {
			alert('textarea');
		}
		else {
			$(this).val(value);
		}
	}
})(jQuery);

(function ($) {
    $.fn.MakeVmTextbox = function (options) {
        var $this = $(this);
        //Default value
        var settings = {
            updateInputName: '',
            className: '',
            repeatDot: 100
        };
        
        //Update value truyen tu ben ngoai vao
        if (options) {
            jQuery.extend(settings, options);
        }
        if($.trim($this.html()) == '') {
            $this.html(repeat('.', settings.repeatDot));
        };
        var keepVal = '';
        if($this.html().replace(/\.+/g,'') != '') {
            keepVal = $this.html();
        };

        var ediableControl = $('<input />', { type: 'text', id: $this.attr('id') + '-editable', name: $this.attr('id'), value: keepVal, className : settings.className });
        ediableControl.hide();
        $this.after(ediableControl);

        $this.click(function () {
            ediableControl.show();
            ediableControl.focus();
            $(this).hide();
            return false;
        });
        ediableControl.click(function () {
            return false;
        });

        var updateContainerValue = function() {
            var str = HtmlEncode(ediableControl.val());
            if (settings.updateInputName != '') {
				$('[name="' + settings.updateInputName + '"]').UpdateInput(str);
            };
            if($.trim(str) != '') {
                $this.html(str);
            }
            else {
                $this.html(repeat('.', settings.repeatDot));
            };
            $this.show();
            ediableControl.hide();
        };

        ediableControl.blur(function () {
            updateContainerValue();
        });

        $(document).click(function () {
            updateContainerValue();
        });
    }
})(jQuery);


(function ($) {
    $.fn.MakeVmOptionbox = function (options) {
        var $this = $(this);
        //Default value
        var settings = {
			className: 'VmOptionbox',
            updateInputName: 'Gender',
            listData: {
				'nam'   : 'Nam',
				'nu'    : 'Nữ'
			},
			onImage: '/Content/images/s16/checkbox.gif',
			offImage: '/Content/images/s16/uncheckbox.gif',
        };
        
        //Update value truyen tu ben ngoai vao
        if (options) {
            jQuery.extend(settings, options);
        }
		
		var controlContainer = $('<div>', { className : settings.className });
		
		$.each(settings.listData, function(key, value) { 
			var sId = $this.attr('id') + '-editable-' + key;
			var itemContainer = $('<div>', { className : $this.attr('id') + '-editable' });
			itemContainer.append($('<input />', { 
				type		: 'image',
				id			: sId, 
				name		: $this.attr('id') + '-' + key, 
				value		: key, 
				className 	: 'VmOptionbox-' + settings.updateInputName,
				src			: settings.offImage
			}));
			itemContainer.append($('<span>', { html : value }).attr('style', 'padding-left:5px;'));
			controlContainer.append(itemContainer);
		});
		
		$this.after(controlContainer);
		
		$('.VmOptionbox-' + settings.updateInputName).click(function(){
			if($(this).attr('src') == settings.offImage) {
				$('.VmOptionbox-' + settings.updateInputName).attr('src', settings.offImage);
				$(this).attr('src', settings.onImage);
				$('[name="' + settings.updateInputName + '"]').UpdateInput($(this).val());
			}
			return false;
		});

    }
})(jQuery);