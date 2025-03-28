public class HTMLPage
{
    public string Title { get; set; }
    public string Body { get; set; }

    public override string ToString()
    {
        return $"<html>\n<head>\n<title>{Title}</title>\n</head>\n<body>\n{Body}\n</body>\n</html>";
    }
}

public interface IHTMLBuilder
{
    void BuildTitle(string title);
    void BuildBody(string body);
    HTMLPage GetResult();
}

public class BasicHTMLBuilder : IHTMLBuilder
{
    private HTMLPage _htmlPage = new HTMLPage();

    public void BuildTitle(string title)
    {
        _htmlPage.Title = title;
    }

    public void BuildBody(string body)
    {
        _htmlPage.Body = body;
    }

    public HTMLPage GetResult()
    {
        return _htmlPage;
    }
}

public class BootstrapHTMLBuilder : IHTMLBuilder
{
    private HTMLPage _htmlPage = new HTMLPage();

    public void BuildTitle(string title)
    {
        _htmlPage.Title = title;
    }

    public void BuildBody(string body)
    {
        _htmlPage.Body = $"<div class='container'>{body}</div>"; 
    }

    public HTMLPage GetResult()
    {
        return _htmlPage;
    }
}

public class MaterialUIHTMLBuilder : IHTMLBuilder
{
    private HTMLPage _htmlPage = new HTMLPage();

    public void BuildTitle(string title)
    {
        _htmlPage.Title = title;
    }

    public void BuildBody(string body)
    {
        _htmlPage.Body = $"<div class='mui-container'>{body}</div>"; 
    }

    public HTMLPage GetResult()
    {
        return _htmlPage;
    }
}

public class HTMLDirector
{
    private IHTMLBuilder _builder;

    public HTMLDirector(IHTMLBuilder builder)
    {
        _builder = builder;
    }

    public void ConstructPage(string title, string body)
    {
        _builder.BuildTitle(title);
        _builder.BuildBody(body);
    }
}

class Program
{
    static void Main(string[] args)
    {
        IHTMLBuilder basicBuilder = new BasicHTMLBuilder();
        HTMLDirector director = new HTMLDirector(basicBuilder);
        director.ConstructPage("Basic Page Title", "This is a basic page body.");
        HTMLPage basicPage = basicBuilder.GetResult();
        Console.WriteLine(basicPage.ToString());

        IHTMLBuilder bootstrapBuilder = new BootstrapHTMLBuilder();
        director = new HTMLDirector(bootstrapBuilder);
        director.ConstructPage("Bootstrap Page Title", "This is a Bootstrap styled body.");
        HTMLPage bootstrapPage = bootstrapBuilder.GetResult();
        Console.WriteLine(bootstrapPage.ToString());

        IHTMLBuilder materialBuilder = new MaterialUIHTMLBuilder();
        director = new HTMLDirector(materialBuilder);
        director.ConstructPage("Material UI Page Title", "This is a Material UI styled body.");
        HTMLPage materialPage = materialBuilder.GetResult();
        Console.WriteLine(materialPage.ToString());
    }
}