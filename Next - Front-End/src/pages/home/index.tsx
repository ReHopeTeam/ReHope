import styles from "./home.module.css";
import Header from "@/components/header/header";
import Footer from "@/components/footer/footer";
import Lista from "@/components/lista/lista";

const Home = () => {
  return (
    <>
      <Header />
      <main className="min_height">
        <section id={styles.section}>
          <Lista />
        </section>
      </main>
      <Footer />
    </>
  );
};

export default Home;
