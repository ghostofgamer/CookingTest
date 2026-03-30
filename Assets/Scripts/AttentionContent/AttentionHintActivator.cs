namespace AttentionContent
{
    public static class AttentionHintActivator
    {
        private static AttentionHintViewer _attentionHintViewer;

        public static void Init(AttentionHintViewer attentionHintViewer)
        {
            _attentionHintViewer = attentionHintViewer;
        }

        public static void ShowHint(string message)
        {
            _attentionHintViewer.ShowAttentionHint(message);
        }
    }
}
