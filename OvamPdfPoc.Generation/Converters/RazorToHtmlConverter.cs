using System.Linq.Expressions;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace OvamPdfPoc.Templates.Converters;

internal class RazorToHtmlConverter(HtmlRenderer renderer)
{
    internal Task<string> RenderAsync<TComponent>(Action<ComponentParameterBuilder<TComponent>>? configureParameters = null)
        where TComponent : IComponent
    {
        return renderer.Dispatcher.InvokeAsync(async () =>
        {
            var parameterBuilder = new ComponentParameterBuilder<TComponent>();
            configureParameters?.Invoke(parameterBuilder);
            var parameterView = parameterBuilder.Build();
            var htmlRoot = await renderer.RenderComponentAsync<TComponent>(parameterView);
            return htmlRoot.ToHtmlString();
        });
    }
}

/// <summary>Builder for a ParameterView to more easily (and in a strongly-typed way) create a set of parameters to pass to a component.</summary>
/// <typeparam name="TComponent">The type </typeparam>
/// <example>If component <c>MyComponent</c> has a parameter <c>MyParameter</c>:
/// <code>
///     new ComponentParameterBuilder&lt;MyComponent&gt;()
///         .WithParameter(c => c.MyParameter, value)
///         .Build();
/// </code></example>
internal class ComponentParameterBuilder<TComponent> where TComponent : IComponent
{
    private readonly Dictionary<string, object?> _componentParameters = new();

    internal ComponentParameterBuilder<TComponent> WithParameter(Expression<Func<TComponent, object>> accessor, object? value)
    {
        if (accessor.Body is not MemberExpression { Member.Name: string name })
        {
            throw new InvalidOperationException("The accessor does not point to a member, or could not find name for accessor");
        }

        _componentParameters.Add(name, value);
        return this;
    }

    internal ParameterView Build() => ParameterView.FromDictionary(_componentParameters);
}