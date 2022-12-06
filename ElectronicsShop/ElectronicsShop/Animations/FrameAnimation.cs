namespace ElectronicsShop.Animations
{
    public class FrameAnimation
    {
        public static void AnimateFrame(Frame frame)
        {
            float hue = frame.BackgroundColor.GetHue();
            float saturation = frame.BackgroundColor.GetSaturation();

            new Animation
        {
            {
                0, 1,
                new Animation (callback: v => frame.BackgroundColor = Color.FromHsla(hue, saturation, v),
                start: 0.29,
                end: 0.18)
            },
            {
                0, 1,
                new Animation(callback: v => frame.Scale = v,
                start: 1.0,
                end: 0.95)
            },
        }.Commit(frame, "Animation", 16, 350, Easing.Linear);
            new Animation
        {
            {
                0, 1,
                new Animation (callback: v => frame.BackgroundColor = Color.FromHsla(hue, saturation, v),
                start: 0.18,
                end: 0.29)
            },
            {
                0, 1,
                new Animation(callback: v => frame.Scale = v,
                start: 0.95,
                end: 1.0)
            },
        }.Commit(frame, "Animation", 16, 350, Easing.Linear);
        }
    }
}
