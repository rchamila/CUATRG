import numpy as np
import os
import sys
import scipy

from scipy import ndimage as ndi
from skimage.color import rgb2gray
from skimage import feature
from skimage.filters import roberts, sobel, scharr, prewitt
from skimage.util import random_noise

rootDir = "D:\\Temp\\Upload\\"
images = []

for root, dirs, files in os.walk(rootDir + "Albums"):  
    for filename in files:
        if  '.csv' not in filename and 'Uploaded' not in root:
            imagePath = root + "\\" + filename #os.path.join(root, filename)
            images.append(imagePath)    
      

for image in images:    
    try:
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
           
        pathGrayscale = rootDir + "Processed\\CM\\Grayscale\\"

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

        jp2k = rootDir + "Processed\\Noise\\Jp2k\\"
        
        if not os.path.exists(jp2k):
            os.makedirs(jp2k)

        imagename = image.split('\\')[-1]
        
       # jp2kImageName =jp2k + imagename.replace("Image_", "Image_Jp2k_" ).replace("jpg","jp2")
        #noise_jp2 = glymur.jp2k.Jp2k(jp2kImageName, im)     

      

        scipy.misc.imsave(pathGrayscale + imagename.replace("Image_", "Image_Grayscale_" ) , img_gray)
        scipy.misc.imsave(pathRoberts + imagename.replace("Image_", "Image_Roberts_" ) , edge_roberts)
        scipy.misc.imsave(pathSobel + imagename.replace("Image_", "Image_Sobel_" ) , edge_sobel)
        scipy.misc.imsave(pathCanny1 + imagename.replace("Image_", "Image_Canny_" ) , edge_canny1)
        scipy.misc.imsave(pathCanny2 + imagename.replace("Image_", "Image_Canny0.2_" ) , edge_canny2)
        #scipy.misc.imsave(pathCanny3 + imagename.replace("Image_", "Image_Canny0.25_" ) , edge_canny3)
        scipy.misc.imsave(pathCanny4 + imagename.replace("Image_", "Image_Canny0.3_" ) , edge_canny4)
        scipy.misc.imsave(pathPrewitt + imagename.replace("Image_", "Image_Prewitt_" ) , edge_prewitt)

        scipy.misc.imsave(gaussian + imagename.replace("Image_", "Image_Gaussian_" ) , noise_gaussian)
        scipy.misc.imsave(salt + imagename.replace("Image_", "Image_Salt_" ) , noise_salt)
        scipy.misc.imsave(pepper + imagename.replace("Image_", "Image_Pepper_" ) , noise_pepper)
        scipy.misc.imsave(speckle + imagename.replace("Image_", "Image_Speckle_" ) , noise_speckle)
        #scipy.misc.imsave(jp2k + image.split('\\')[-1] , noise_jp2)

        print("Image processing completed for :" , image.split('\\')[-1])
    except Exception as ex:
        print("Unexpected error occured processing image :" , image.split('\\')[-1] , " Error : ", sys.exc_info()[0])
    





    