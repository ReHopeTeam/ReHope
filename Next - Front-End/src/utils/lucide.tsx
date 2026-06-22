import {
  Search,
  ArrowDownZA,
  ArrowDownAZ,
  ChartNoAxesColumnDecreasing,
  ChartNoAxesColumnIncreasing,
  SquarePen,
  Eye,
  EyeOff,
  LucideProps,
  Delete,
  Filter,
  Upload,
  Tag,
  RectangleEllipsis,
  RulerDimensionLine,
  MapPin,
  MapPinPlus,
  User,
  ALargeSmall,
  MessageSquareText,
  Grid2X2,
  Grid2X2Plus,
  Lock,
  Phone,
  Mail,
  ShieldEllipsis,
  ShieldCheck,
  ShieldX,
  Menu,
  X,
  NotepadText,
  HeartPlus,
  UserRoundPlus,
  History,
  HouseHeart,
  Package,
  PackagePlus,
  MailPlus,
  CircleQuestionMark,
  LogIn,
  ChevronUp,
  ChevronDown,
} from "lucide-react";

const icons = {
  Search,
  ArrowDownZA,
  ArrowDownAZ,
  ChartNoAxesColumnDecreasing,
  ChartNoAxesColumnIncreasing,
  Delete,
  SquarePen,
  Eye,
  EyeOff,
  Filter,
  Upload,
  Tag,
  RectangleEllipsis,
  RulerDimensionLine,
  MapPin,
  MapPinPlus,
  User,
  ALargeSmall,
  MessageSquareText,
  Grid2X2,
  Grid2X2Plus,
  Lock,
  Phone,
  Mail,
  ShieldEllipsis,
  ShieldCheck,
  ShieldX,
  Menu,
  X,
  NotepadText,
  HeartPlus,
  UserRoundPlus,
  History,
  HouseHeart,
  Package,
  PackagePlus,
  MailPlus,
  CircleQuestionMark,
  LogIn,
  ChevronUp,
  ChevronDown,
};

type IconName = keyof typeof icons;

interface IconProps extends Omit<LucideProps, "name"> {
  name?: IconName | null;
}

export default function Icon({
  name,
  size = 20,
  strokeWidth = 2,
  ...props
}: IconProps) {
  // 1. Se o nome não foi passado, for nulo ou não existir no mapeamento, previne o erro retornando null
  if (!name || !icons[name]) {
    return null;
  }

  const LucideIcon = icons[name];

  // 2. Só renderiza se for um componente válido
  return <LucideIcon size={size} strokeWidth={strokeWidth} {...props} />;
}
