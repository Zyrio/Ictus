<?php
$root = '';
//$root = $_SERVER['DOCUMENT_ROOT']; // use if specifying path from root
$path = './';
function getImagesFromDir($path) {
    $images = array();
    if ( $img_dir = @opendir($path) ) {
        while ( false !== ($img_file = readdir($img_dir)) ) {
            // checks for gif, jpg, png
            if ( preg_match("/(\.gif|\.jpg|\.png)$/", $img_file) ) {
                $images[] = $img_file;
            }
        }
        closedir($img_dir);
    }
    return $images;
}
function getRandomFromArray($ar) {
    mt_srand( (double)microtime() * 1000000 ); // php 4.2+ not needed
    $num = array_rand($ar);
    return $ar[$num];
}
// Obtain list of images from directory 
$imgList = getImagesFromDir($root . $path);
$img = getRandomFromArray($imgList);
?>
<!DOCTYPE html>
<html>
<head>
    <title><?php echo($img); ?></title>
    <style>
        html,
        body {
            height: 100%;
            margin: 0px;
            padding: 0px;
            background-color: #111111;
        }
        .site-wrapper {
            display: table;
            width: 100%;
            height: 100%; /* For at least Firefox */
            min-height: 100%;
            max-width: 100%;
            padding: 0px;
            margin: 0px;
        }
        .site-wrapper-inner {
            display: table-cell;
            vertical-align: middle;
            text-align: center;
        }
        img {
            max-width: 100%;
            max-height: 100%;
            -webkit-box-shadow: 0px 0px 45px 0px rgba(0,0,0,0.75);
            -moz-box-shadow: 0px 0px 45px 0px rgba(0,0,0,0.75);
            box-shadow: 0px 0px 45px 0px rgba(0,0,0,0.75);
            -webkit-box-sizing: border-box;
            -moz-box-sizing: border-box;
            margin: 0 auto;
            display: block;
            background-color: white;
            cursor: pointer;
        }
    </style>
</head>
<body>
    <div class="site-wrapper"><div class="site-wrapper-inner">
        <a href="."><img src="<?php echo $path . $img ?>" alt="" /></a>
    </div></div>
</body>
</html>