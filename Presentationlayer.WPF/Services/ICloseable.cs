namespace Presentationlayer.WPF.Services
{
    public interface ICloseable
    {
        void Close();
        bool? DialogResult { get; set; }
    }
}