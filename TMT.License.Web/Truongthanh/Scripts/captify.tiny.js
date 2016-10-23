jQuery.fn.extend({
    captify: function (n) {
        var a = $.extend({
            speedOver: "fast",
            speedOut: "normal",
            hideDelay: 500,
            animation: "slide",
            prefix: "",
            opacity: "0.45",
            className: "caption-bottom",
            position: "bottom",
            spanWidth: "100%",
			width:0, //Nhu them vao vi Bi Adblock Plus chan
			width:0 //Nhu them vao vi Bi Adblock Plus chan
        }, n);
        $(this).each(function () {
            var c = this;
            $(this).load(function () {
                if (c.hasInit) return false;
                c.hasInit = true;
                var i = false,
                    k = false,
                    e = $("#" + $(this).attr("rel")),
                    g = !e.length ? $(this).attr("alt") : e.html();
                e.remove();
                e = this.parent && this.parent.tagName == "a" ? this.parent : $(this);
                var h = e.wrap("<div></div>").parent().css({
                    overflow: "hidden",
                    padding: 0,
                    fontSize: 0.1
				//}).addClass("caption-wrapper").width($(this).width()).height($(this).height());
                }).addClass("caption-wrapper").width(a.width>0?a.width:$(this).width()).height(a.height>0?a.height:$(this).height()); //Nhu sua vao vi Bi Adblock Plus chan
                $.map(["top", "right", "bottom", "left"], function (f) {
                    h.css("margin-" + f, $(c).css("margin-" + f));
                    $.map(["style", "width", "color"], function (j) {
                        j = "border-" + f + "-" + j;
                        h.css(j, $(c).css(j))
                    })
                });
                $(c).css({
                    border: "0 none"
                });
                var b = $("div:last", h.append("<div></div>")).addClass(a.className),
                    d = $("div:last", h.append("<div></div>")).addClass(a.className).append(a.prefix).append(g);
                $("*", h).css({
                    margin: 0
                }).show();
                g = jQuery.browser.msie ? "static" : "relative";
                b.css({
                    zIndex: 1,
                    position: g,
                    opacity: a.animation == "fade" ? 0 : a.opacity,
                    width: a.spanWidth
                });
                if (a.position == "bottom") {
                    e = parseInt(b.css("border-top-width").replace("px", "")) + parseInt(d.css("padding-top").replace("px", "")) - 1;
                    d.css("paddingTop", e)
                }
                d.css({
                    position: g,
                    zIndex: 2,
                    background: "none",
                    border: "0 none",
                    opacity: a.animation == "fade" ? 0 : 1,
                    width: a.spanWidth
                });
                b.width(d.outerWidth());
                b.height(d.height());
                g = a.position == "bottom" && jQuery.browser.msie ? -4 : 0;
                var l = a.position == "top" ? {
                    hide: -$(c).height() - b.outerHeight() - 1,
                    show: -$(c).height()
                } : {
                    hide: 0,
                    show: -b.outerHeight() + g
                };
                d.css("marginTop", -b.outerHeight());
                b.css("marginTop", l[a.animation == "fade" || a.animation == "always-on" ? "show" : "hide"]);
                var m = function () {
                        if (!i && !k) {
                            var f = a.animation == "fade" ? {
                                opacity: 0
                            } : {
                                marginTop: l.hide
                            };
                            b.animate(f, a.speedOut);
                            a.animation == "fade" && d.animate({
                                opacity: 0
                            }, a.speedOver)
                        }
                    };
                if (a.animation != "always-on") {
					if (!i) {
						var f = a.animation == "fade" ? {
							opacity: a.opacity
						} : {
							marginTop: l.show
						};
						b.animate(f, a.speedOver);
						a.animation == "fade" && d.animate({
							opacity: 1
						}, a.speedOver / 2);
						k = false;
                        window.setTimeout(m, 7000)
					}
                    $(this).hover(function () {
                        k = true;
                        if (!i) {
                            var f = a.animation == "fade" ? {
                                opacity: a.opacity
                            } : {
                                marginTop: l.show
                            };
                            b.animate(f, a.speedOver);
                            a.animation == "fade" && d.animate({
                                opacity: 1
                            }, a.speedOver / 2)
                        }
                    }, function () {
                        k = false;
                        window.setTimeout(m, a.hideDelay)
                    });
                    $("div", h).hover(function () {
                        i = true
                    }, function () {
                        i = false;
                        window.setTimeout(m, a.hideDelay)
                    })
                }
            });
            if (this.complete || this.naturalWidth > 0) $(c).trigger("load")
        })
    }
});