using System;
using System.IO;
using Serilog.Events;
using Serilog.Formatting;
using Serilog.Formatting.Json;

namespace PROJECT_NAME.Api.Middleware.Logging
{
    /// <summary>
    /// An <see cref="ITextFormatter"/> that writes events in a compact JSON format.
    /// </summary>
    public class JsonLogFormatter : ITextFormatter
    {
        readonly JsonValueFormatter _formatter = new JsonValueFormatter(typeTagName: "$type");

        /// <summary>
        /// Format the log event into the output. Subsequent events will be newline-delimited.
        /// </summary>
        /// <param name="logEvent">The event to format.</param>
        /// <param name="output">The output.</param>
        public void Format(LogEvent logEvent, TextWriter output)
        {
            FormatEvent(logEvent, output, _formatter);
            output.WriteLine();
        }

        /// <summary>
        /// Format the log event into the output.
        /// </summary>
        /// <param name="logEvent">The event to format.</param>
        /// <param name="output">The output.</param>
        /// <param name="formatter">A value formatter for <see cref="LogEventPropertyValue"/>s on the event.</param>
        private static void FormatEvent(LogEvent logEvent, TextWriter output, JsonValueFormatter formatter)
        {
            if (logEvent == null) throw new ArgumentNullException(nameof(logEvent));
            if (output == null) throw new ArgumentNullException(nameof(output));
            if (formatter == null) throw new ArgumentNullException(nameof(formatter));

            output.Write("{\"@t\":\"");
            output.Write(logEvent.Timestamp.UtcDateTime.ToString("O"));

            var formattedMessage = logEvent.MessageTemplate.Render(logEvent.Properties);
            output.Write("\",\"@r\":");
            JsonValueFormatter.WriteQuotedJsonString(formattedMessage, output);

            output.Write(",\"@mt\":");
            JsonValueFormatter.WriteQuotedJsonString(logEvent.MessageTemplate.Text, output);

            output.Write(",\"@l\":\"");
            output.Write(logEvent.Level);
            output.Write('\"');

            if (logEvent.Exception != null)
            {
                output.Write(",\"@x\":");
                JsonValueFormatter.WriteQuotedJsonString(logEvent.Exception.ToString(), output);
            }

            foreach (var property in logEvent.Properties)
            {
                var name = property.Key;
                if (name.Length > 0 && name[0] == '@')
                {
                    // Escape first '@' by doubling
                    name = '@' + name;
                }

                output.Write(',');
                JsonValueFormatter.WriteQuotedJsonString(name, output);
                output.Write(':');
                formatter.Format(property.Value, output);
            }

            output.Write('}');
        }
    }
}
