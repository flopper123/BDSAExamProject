module.exports = {
  purge: {
    enabled: true,
    content: [
        './**/*.html',
        './**/*.razor'
    ],
  },
  darkMode: false, // or 'media' or 'class'
  theme: {
    extend: {
      boxShadow: {
        white: '2px 2px 0px 0px rgba(255, 255, 255, 0.8)',
      },
    },
  },
  variants: {
    extend: {},
  },
  plugins: [],
}
