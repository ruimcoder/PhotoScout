namespace PhotoScouterWeb
{
    using System.Web.Mvc;
    using PhotoScouterWeb.Constants;
    using NWebsec.Csp;
    using NWebsec.Mvc.HttpHeaders;
    using NWebsec.Mvc.HttpHeaders.Csp;

    public static class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            AddSecurityFilters(filters);
            AddContentSecurityPolicyFilters(filters);
        }

        /// <summary>
        /// Several NWebsec Security Filters are added here. 
        /// (See <see cref="http://www.dotnetnoob.com/2012/09/security-through-http-response-headers.html"/> and 
        /// <see cref="https://github.com/NWebsec/NWebsec/wiki"/> for more information).
        /// Note: All of these filters can be applied to individual controllers and actions and indeed
        /// some of them only make sense when applied to a controller or action instead of globally here.
        /// </summary>
        private static void AddSecurityFilters(GlobalFilterCollection filters)
        {
            // Require HTTPS to be used accross the whole site.
            // filters.Add(new RequireHttpsAttribute());

            // Cache-Control: no-cache, no-store, must-revalidate 
            // Expires: -1 
            // Pragma: no-cache
            //      Specifies whether appropriate headers to prevent browser caching should be set in the HTTP response.
            //      Do not apply this attribute here globally, use it sparingly to disable caching. A good place to 
            //      use this would be on a page where you want to post back credit card information because caching 
            //      credit card information could be a security risk.
            // filters.Add(new SetNoCacheHttpHeadersAttribute());   

            // X-Robots-Tag - Adds the X-Robots-Tag HTTP header. Disable robots from any action or controller this attribute is applied to.
            // filters.Add(new XRobotsTagAttribute() { NoIndex = true, NoFollow = true }); 

            // X-Content-Type-Options - Adds the X-Content-Type-Options HTTP header. Stop IE9 and below from sniffing files and overriding the Content-Type header (MIME type).
            filters.Add(new XContentTypeOptionsAttribute());

            // X-Download-Options - Adds the X-Download-Options HTTP header. When users save the page, stops them from opening it and forces a save and manual open.
            filters.Add(new XDownloadOptionsAttribute());

            // X-Frame-Options - Adds the X-Frame-Options HTTP header. Stop clickjacking by stopping the page from opening in an iframe or only allowing it from the same origin.  
            //      Deny - Specifies that the X-Frame-Options header should be set in the HTTP response, instructing the browser to display the page when it is loaded in an iframe - but only if the iframe is from the same origin as the page.
            //      SameOrigin - Specifies that the X-Frame-Options header should be set in the HTTP response, instructing the browser to not display the page when it is loaded in an iframe.
            //      Disabled - Specifies that the X-Frame-Options header should not be set in the HTTP response.
            filters.Add(
                new XFrameOptionsAttribute()
                {
                    Policy = XFrameOptionsPolicy.Deny
                });
        }

        /// <summary>
        /// Adds the Content-Security-Policy (CSP) and/or Content-Security-Policy-Report-Only HTTP headers.
        /// This creates a whitelist from where various content in a webpage can be loaded from. (See
        /// <see cref="https://developer.mozilla.org/en-US/docs/Web/Security/CSP"/> and
        /// <see cref="http://www.dotnetnoob.com/2012/09/security-through-http-response-headers.html"/> and 
        /// <see cref="https://github.com/NWebsec/NWebsec/wiki"/> and 
        /// <see cref="https://developer.mozilla.org/en-US/docs/Web/Security/CSP/CSP_policy_directives"/> for more information).
        /// Note: CSP 2.0 has drafted a recommendation by the W3C (See <see cref="http://www.w3.org/TR/CSP2/"/> for more information).
        /// Note: Not all browsers support this yet but most now do (See http://caniuse.com/#search=CSP for a list).
        /// Note: If you are using the 'Browser Link' feature of the Webs Essentials Visual Studio extension, it will not work
        /// if you enable CSP (See <see cref="http://webessentials.uservoice.com/forums/140520-general/suggestions/6665824-browser-link-support-for-content-security-policy"/>).
        /// Note: All of these filters can be applied to individual controllers and actions e.g. If an action requires
        /// access to content from YouTube.com, then you can add the following attribute to the action:
        /// [CspFrameSrc(CustomSources = "youtube.com")].
        /// </summary>
        private static void AddContentSecurityPolicyFilters(GlobalFilterCollection filters)
        {
            // Content-Security-Policy - Add the Content-Security-Policy HTTP header to enable Content-Security-Policy.
            filters.Add(new CspAttribute());
            // OR
            // Content-Security-Policy-Report-Only - Add the Content-Security-Policy-Report-Only HTTP header to enable logging of 
            //      violations without blocking them. This is good for testing CSP without enabling it.
            //      To make use of this attribute, rename all the attributes below to their ReportOnlyAttribute versions e.g. CspDefaultSrcAttribute 
            //      becomes CspDefaultSrcReportOnlyAttribute.
            // filters.Add(new CspReportOnlyAttribute());


            // Enables logging of CSP violations. See the NWebsecHttpHeaderSecurityModule_CspViolationReported method in Global.asax.cs to 
            // see where they are logged.
            filters.Add(new CspReportUriAttribute() { EnableBuiltinHandler = true });


            // default-src - Sets a default source list for a number of directives. If the other directives below are not used 
            //               then this is the default setting.
            filters.Add(
                new CspDefaultSrcAttribute()
                {
                    // Disallow everything from the same domain by default.
                    None = true,
                    // Allow everything from the same domain by default.
                    // Self = true
                });


            // base-uri - This directive restricts the document base URL (http://www.w3.org/TR/html5/infrastructure.html#document-base-url).
            filters.Add(
                new CspBaseUriAttribute()
                {
                    // Allow base URL's from example.com.
                    // CustomSources = "example.com",
                    // Allow base URL's from the same domain.
                    Self = false
                });
            // child-src - This directive restricts from where the protected resource can load web workers or embed frames.
            //             This was introduced in CSP 2.0 to replace frame-src. frame-src should still be used for older browsers.
            filters.Add(
                new CspChildSrcAttribute()
                {
                    // Allow web workers or embed frames from example.com.
                    // CustomSources = "example.com",
                    // Allow web workers or embed frames from the same domain.
                    Self = false
                });
            // connect-src - This directive restricts which URIs the protected resource can load using script interfaces 
            //               (Ajax Calls and Web Sockets).
            filters.Add(
                new CspConnectSrcAttribute()
                {
                    // Allow AJAX and Web Sockets to example.com.
                    // CustomSources = "example.com",
                    // Allow all AJAX and Web Sockets calls from the same domain.
                    Self = true
                });
            // font-src - This directive restricts from where the protected resource can load fonts.
            filters.Add(
                new CspFontSrcAttribute()
                {
                    // Allow fonts from example.com.
                    // CustomSources = "example.com",
                    // Allow all fonts from the same domain.
                    Self = true
                });
            // form-action - This directive restricts which URLs can be used as the action of HTML form elements.
            filters.Add(
                new CspFormActionAttribute()
                {
                    // Allow forms to post back to example.com.
                    // CustomSources = "example.com",
                    // Allow forms to post back to the same domain.
                    Self = true
                });
            // frame-src - This directive restricts from where the protected resource can embed frames.
            //             This is now deprecated in favour of child-src but should still be used for older browsers.
            filters.Add(
                new CspFrameSrcAttribute()
                {
                    // Allow iFrames from example.com.
                    // CustomSources = "example.com",
                    // Allow iFrames from the same domain.
                    Self = false
                });
            // frame-ancestors - This directive restricts from where the protected resource can embed frame, iframe, object, embed or applet's.
            filters.Add(
                new CspFrameAncestorsAttribute()
                {
                    // Allow frame, iframe, object, embed or applet's from example.com.
                    // CustomSources = "example.com",
                    // Allow frame, iframe, object, embed or applet's from the same domain.
                    Self = false
                });
            // img-src - This directive restricts from where the protected resource can load images.
            filters.Add(
                new CspImgSrcAttribute()
                {
                    // Allow images from example.com.
                    // CustomSources = "example.com",
                    // Allow images from the same domain.
                    Self = true,
                });
            // script-src - This directive restricts which scripts the protected resource can execute. 
            //              The directive also controls other resources, such as XSLT style sheets, which can cause the user agent to execute script.
            filters.Add(
                new CspScriptSrcAttribute()
                {
                    // Allow scripts from the CDN's.
                    CustomSources = string.Format("{0} {1}", ContentDeliveryNetwork.Google.Domain, ContentDeliveryNetwork.Microsoft.Domain),
                    // Allow scripts from the same domain.
                    Self = true,
                    // Allow the use of the eval() method to create code from strings. This is unsafe and can open your site up to XSS vulnerabilities.
                    // UnsafeEval = true,
                    // Allow inline JavaScript, this is unsafe and can open your site up to XSS vulnerabilities.
                    // UnsafeInline = true
                });
            // media-src - This directive restricts from where the protected resource can load video and audio.
            filters.Add(
                new CspMediaSrcAttribute()
                {
                    // Allow audio and video from example.com.
                    // CustomSources = "example.com",
                    // Allow audio and video from the same domain.
                    Self = false
                });
            // object-src - This directive restricts from where the protected resource can load plugins.
            filters.Add(
                new CspObjectSrcAttribute()
                {
                    // Allow plugins from example.com.
                    // CustomSources = "example.com",
                    // Allow plugins from the same domain.
                    Self = false
                });
            // plugin-types - This directive restricts the set of plugins that can be invoked by the protected resource.
            //                This directive is not currently supported by NWebSec (See https://github.com/NWebsec/NWebsec/issues/53).
            // style-src - This directive restricts which styles the user applies to the protected resource.
            filters.Add(
                new CspStyleSrcAttribute()
                {
                    // Allow CSS from example.com
                    // CustomSources = "example.com",
                    // Allow CSS from the same domain.
                    Self = true,
                    // Allow inline CSS, this is unsafe and can open your site up to XSS vulnerabilities.
                    // Note: This is currently enable because Modernizr does not support CSP and includes inline styles
                    // in its JavaScript files. This is a security hold. If you don't want to use Modernizr, 
                    // be sure to disable unsafe inline styles. For more information see:
                    // http://stackoverflow.com/questions/26532234/modernizr-causes-content-security-policy-csp-violation-errors
                    // https://github.com/Modernizr/Modernizr/pull/1263
                    UnsafeInline = true
                });
        }
    }
}
