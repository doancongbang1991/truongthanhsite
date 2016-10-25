/***************************************************************
 * Coder: Phan Nguyên Như (nhuphan@vietmeta.com, +84986030757) *
 ***************************************************************/
(function ($) {
    $.fn.MakeVmSelectbox = function (options) {
        var $this = $(this);
        //Default value
        var settings = {
            width: $this.outerWidth(),              //Chieu rong cua selectBox
            className: 'VmSelect',                       //Classname
            html: '<div class="selectBox"></div>'   //SelectBox element
        };

        //Update value truyen tu ben ngoai vao
        if (options) {
            jQuery.extend(settings, options);
        }

        var selectBoxContainer = $('<div>', settings);

        var dropDown = $('<ul>', { className: 'dropDown' });
        var selectBox = selectBoxContainer.find('.selectBox');

        // Looping though the options of the original select element

        $this.find('option').each(function (i) {
            var option = $(this);

            if (i == $this.attr('selectedIndex')) {
                selectBox.html(option.text());
            }

            // As of jQuery 1.4.3 we can access HTML5 
            // data attributes with the data() method.

            //		    if(option.data('skip')){
            //			    return true;
            //		    }

            // Creating a dropdown item according to the
            // data-icon and data-html-text HTML5 attributes:

            var li = $('<li>', {
                html: '<span>' + option.html() + '</span>'
            });

            li.click(function () {
                selectBox.html(option.text());
                // When a click occurs, we are also reflecting
                // the change on the original select element:

                $this.val(option.val());
                //$('#' + $this.attr('id')).val(option.val()); //Sẽ tạo hidden input có cùng ID và remove element $this

                dropDown.trigger('hide');

                return false;
            });

            dropDown.append(li);
        });

        
        selectBoxContainer.append(dropDown.hide());
        $this.hide().after(selectBoxContainer);

        //Tạo hidden input có cùng ID và remove element $this
//        var hidden_attr = {
//            id: $this.attr('id'),
//            name: $this.attr('name'),
//            type: 'hidden',
//            value: $this.val()
//        };

//        var hiddenField = $('<input />', hidden_attr);
//        $this.remove();
//        selectBoxContainer.append(hiddenField);

        // Binding custom show and hide events on the dropDown:

        dropDown.bind('show', function () {

            if (dropDown.is(':animated')) {
                return false;
            }

            selectBox.addClass('expanded');
            dropDown.slideDown();

        }).bind('hide', function () {

            if (dropDown.is(':animated')) {
                return false;
            }

            selectBox.removeClass('expanded');
            dropDown.slideUp();

        }).bind('toggle', function () {
            if (selectBox.hasClass('expanded')) {
                dropDown.trigger('hide');
            }
            else dropDown.trigger('show');
        });

        selectBox.click(function () {
            dropDown.trigger('toggle');
            return false;
        });

        // If we click anywhere on the page, while the
        // dropdown is shown, it is going to be hidden:

        $(document).click(function () {
            dropDown.trigger('hide');
        });
    }
})(jQuery);