export default function DeferMixin(count = 10) {
  return {
    data() {
      return {
        displayPriority: 0,
        displayPriorityTime: 0,
      };
    },
    mounted() {
      this.runDisplayPriority();
      this.runDisplayPrioritywithTime();
    },
    methods: {
      runDisplayPriority() {
        const step = () => {
          requestAnimationFrame(() => {
            this.displayPriority++;
            if (this.displayPriority < count) {
              step();
            }
          });
        };
        step();
      },
      runDisplayPrioritywithTime() {
        const step = () => {
          setTimeout(() => {
            this.displayPriorityTime += 50;
            if (this.displayPriorityTime < count * 50) {
              step();
            }
          }, 50);
        };
        step();
      },
      defer(priority) {
        return this.displayPriority >= priority;
      },
      deferT(priority) {
        return Math.ceil(this.displayPriorityTime / 50) >= priority;
      },
    },
  };
}
