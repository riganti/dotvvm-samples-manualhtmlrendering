# Manual HTML rendering in DotVVM

This sample shows how to render a [DotVVM](https://github.com/riganti/dotvvm) control into HTML manually, e. g. for the purpose of converting the HTML to PDF or other format.

## Rendering the control

To render HTML manually, you need to create a new `HtmlWriter` and call the `Render` method on the control.

It is recommended to set `RenderSettings.Mode` to `Server` in order to make DotVVM render all texts, link URLs and GridView/Repeater items in HTML. 
There will still be Knockout JS comments and data-bind attributes, but the output should be displayed correctly even without running JavaScript.

To be able to access the control in the viewmodel, you need to add the `ID` attribute to the control. 
Then, you can use this code to find the control:

```
    var grid = Context.View.FindControlByClientId<GridView>("MyGrid");
    grid.SetValue(RenderSettings.ModeProperty, RenderMode.Server);
```

If you need to render the entire page, you can just call the `Render` method on `Context.View` which represents the page root element.

To render HTML, you need to create `HtmlWriter` and pass it into the `Render` function along with the `Context`:

```
    using (var stringWriter = new StringWriter())
    {
        var writer = new HtmlWriter(stringWriter, Context);

        grid.Render(writer, Context);

        OutputHtml = stringWriter.ToString();
    }
```
