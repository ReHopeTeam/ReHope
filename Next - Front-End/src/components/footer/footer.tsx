import Link from "next/link";
import styles from "./footer.module.css";
import { useRouter } from "next/router";

const Footer = () => {
  const router = useRouter();

  return (
    <footer id={styles.footer} className="main_footer">
      <div className="container row">
        {/* <div> do botão que redireciona para a home */}
        <div>
          <Link href="/home">
            <img
              className="img"
              id={styles.img}
              src="/imgs/LogoBranca.svg"
              alt="Logo do site"
            />
          </Link>
        </div>
        {/* <div> do botão que redireciona para a home */}
        <div className="row">
          <Link
            className={styles.social_link}
            href="https://www.instagram.com"
            target="_blank"
            rel="noopener noreferrer"
          >
            <img
              className={styles.social_icon}
              src="/imgs/Instagram.svg"
              alt="Instagram da ReHope"
            />
          </Link>
          <Link
            className={styles.social_link}
            href="https://www.facebook.com"
            target="_blank"
            rel="noopener noreferrer"
          >
            <img
              className={styles.social_icon}
              src="/imgs/Facebook.svg"
              alt="Facebook da ReHope"
            />
          </Link>
        </div>
      </div>
    </footer>
  );
};

export default Footer;
