import numpy as np
import os
import sys
import scipy
import cv2
import glymur

from scipy import ndimage as ndi
from skimage.color import rgb2gray
from skimage import feature
from skimage.filters import roberts, sobel, scharr, prewitt
from skimage.util import random_noise

#Manullay modified
#C:\Users\Chamila\AppData\Local\conda\conda\envs\CUATRG2\Lib\site-packages\scipy\misc\pilutil.py
#Line 97
#cscale = cmax - cmin (Removed )
#cscale = numpy.subtract(cmax, cmin, dtype=numpy.float32)
#Line 105
#bytedata = (data - cmin) * scale + low (Removed )
#bytedata = numpy.subtract(data, cmin, dtype=numpy.float32) * scale + low

rootDir = "D:\\Temp\\UploadTemp\\"
images = []

for root, dirs, files in os.walk(rootDir + "Albums"):  
    for filename in files:
        if  '.csv' not in filename and 'Uploaded' not in root and 'Blurred' not in root:
            imagePath = root + "\\" + filename #os.path.join(root, filename)
            images.append(imagePath)    
      

for image in images:    
    #try:
    im = scipy.misc.imread(image)

    img_gray = rgb2gray(im) 

    edge_roberts = roberts(img_gray)
    edge_sobel = sobel(img_gray)
    edge_canny1 = feature.canny(img_gray)
    edge_canny2 = feature.canny(img_gray, sigma=0.2)
    edge_canny3 = feature.canny(img_gray, sigma=0.25)
    edge_canny4 = feature.canny(img_gray, sigma=0.3)
    edge_prewitt = prewitt(img_gray)

        
      
    noise_gaussian = random_noise(im, "gaussian") 
    noise_salt = random_noise(im, "salt") 
    noise_pepper = random_noise(im, "pepper") 
    noise_speckle = random_noise(im, "speckle") 

    blur_avg = cv2.blur(im,(5,5))
    blur_gaussian =  cv2.GaussianBlur(im,(5,5),0)
    blur_median = cv2.medianBlur(im,5)


           
    pathGrayscale = rootDir + "Processed\\Color\\Grayscale\\"

    if not os.path.exists(pathGrayscale):
        os.makedirs(pathGrayscale)
    
    pathRoberts = rootDir + "Processed\\Edge\\Roberts\\"

    if not os.path.exists(pathRoberts):
        os.makedirs(pathRoberts)

    pathSobel = rootDir + "Processed\\Edge\\Sobel\\"

    if not os.path.exists(pathSobel):
        os.makedirs(pathSobel)

    pathCanny1 = rootDir + "Processed\\Edge\\Canny1.0\\"

    if not os.path.exists(pathCanny1):
        os.makedirs(pathCanny1)

    pathCanny2 = rootDir + "Processed\\Edge\\Canny0.2\\"

    if not os.path.exists(pathCanny2):
        os.makedirs(pathCanny2)

    #pathCanny3 = rootDir + "Processed\\Edge\\Canny0.25\\"

    #if not os.path.exists(pathCanny3):
    #    os.makedirs(pathCanny3)

    pathCanny4 = rootDir + "Processed\\Edge\\Canny0.3\\"

    if not os.path.exists(pathCanny4):
        os.makedirs(pathCanny4)

    pathPrewitt = rootDir + "Processed\\Edge\\Prewitt\\"

    if not os.path.exists(pathPrewitt):
        os.makedirs(pathPrewitt)

    gaussian = rootDir + "Processed\\Noise\\Gaussian\\"

    if not os.path.exists(gaussian):
        os.makedirs(gaussian)

    salt = rootDir + "Processed\\Noise\\Salt\\"

    if not os.path.exists(salt):
        os.makedirs(salt)

    pepper = rootDir + "Processed\\Noise\\Pepper\\"

    if not os.path.exists(pepper):
        os.makedirs(pepper)

    speckle = rootDir + "Processed\\Noise\\Speckle\\"

    if not os.path.exists(speckle):
        os.makedirs(speckle)

    jp2k = rootDir + "Processed\\Noise\\JPEG2000\\"
        
    if not os.path.exists(jp2k):
        os.makedirs(jp2k)

    jpeg = rootDir + "Processed\\Noise\\JPEG\\"
        
    if not os.path.exists(jpeg):
        os.makedirs(jpeg)

    blur = rootDir + "Processed\\Blur\\Average\\"

    if not os.path.exists(blur):
        os.makedirs(blur)

    gaussianblur = rootDir + "Processed\\Blur\\Gaussian\\"

    if not os.path.exists(gaussianblur):
        os.makedirs(gaussianblur)
    
    medianblur = rootDir + "Processed\\Blur\\Median\\"

    if not os.path.exists(medianblur):
        os.makedirs(medianblur)


    imagename = image.split('\\')[-1]
        
    jp2kImageName =jp2k + imagename.replace("Image_", "Image_Noise_Jpeg2000_" ).replace("jpg","jp2k")
    noise_jp2 = glymur.jp2k.Jp2k(jp2kImageName, im)     

    scipy.misc.imsave(pathGrayscale + imagename.replace("Image_", "Image_Color_Grayscale_" ) , img_gray)
    scipy.misc.imsave(pathGrayscale + imagename.replace("Image_", "Image_Color_Grayscale_" ) , img_gray)
    scipy.misc.imsave(pathRoberts + imagename.replace("Image_", "Image_Edge_Roberts_" ) , edge_roberts)
    scipy.misc.imsave(pathSobel + imagename.replace("Image_", "Image_Edge_Sobel_" ) , edge_sobel)
    scipy.misc.imsave(pathCanny1 + imagename.replace("Image_", "Image_Edge_Canny_" ) , edge_canny1)
    scipy.misc.imsave(pathCanny2 + imagename.replace("Image_", "Image_Edge_Canny0.2_" ) , edge_canny2)
    #scipy.misc.imsave(pathCanny3 + imagename.replace("Image_", "Image_Canny0.25_" ) , edge_canny3)
    scipy.misc.imsave(pathCanny4 + imagename.replace("Image_", "Image_Edge_Canny0.3_" ) , edge_canny4)
    scipy.misc.imsave(pathPrewitt + imagename.replace("Image_", "Image_Edge_Prewitt_" ) , edge_prewitt)

    scipy.misc.imsave(gaussian + imagename.replace("Image_", "Image_Noise_Gaussian_" ) , noise_gaussian)
    scipy.misc.imsave(salt + imagename.replace("Image_", "Image_Noise_Salt_" ) , noise_salt)
    scipy.misc.imsave(pepper + imagename.repl-("Image_", "Image_Noise_Speckle_" ) , noise_speckle)
    #scipy.misc.imsave(jpeg + imagename.replace("Image_", "Image_Noise_JPEG_" ) , im)  

    scipy.misc.imsave(blur + imagename.replace("Image_", "Image_Blur_Average_" ) , blur_avg)
    scipy.misc.imsave(gaussianblur + imagename.replace("Image_", "Image_Blur_Gaussian_" ) , blur_gaussian)
    scipy.misc.imsave(medianblur + imagename.replace("Image_", "Image_Blur_Median_" ) , blur_median)

    print("Image processing completed for :" , image.split('\\')[-1])
    #except Exception as ex:
        #print("Unexpected error occured processing image :" , image.split('\\')[-1] , " Error : ", sys.exc_info()[0])
print("Finished processing all the images");
    





    